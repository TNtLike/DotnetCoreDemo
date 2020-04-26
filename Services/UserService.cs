using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MyWebApi.Services
{
    public class UserService : IBaseService<User>, IAuthenticateService<User>
    {
        private readonly IMongoCollection<User> _users;
        private readonly TokenManagement _token;
        public UserService(MongoDBSettings config, TokenManagement token)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _users = databases.GetCollection<User>(nameof(User));
            _token = token;
        }

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public async Task<ServiceResponse> GetUserAsync(SignInRequest req)
        {
            try
            {
                User userIn = await _users.Find<User>(user => user.Username == req.Username || user.Telephone == req.Username || user.Email == req.Username).FirstOrDefaultAsync();
                if (userIn == null)
                {
                    return new ServiceResponse(false, "An error occurred : No Such User.");
                }
                else
                {
                    if (userIn.Password == req.Password)
                    {
                        string token = GetAuthenticated(userIn);
                        return token == string.Empty ? new ServiceResponse(false, "An error occurred : Get No Token.") : new ServiceResponse(true, token);
                    }
                    return new ServiceResponse(false, "An error occurred : Wrong Password.");
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}");
            }
        }

        public ServiceResponse Create(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Id))
                {
                    return new ServiceResponse(false, $"An error occurred : Error Data.");
                }
                _users.InsertOne(user);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }
        }
        public async Task<ServiceResponse> CreateAsync(SignUpRequest req)
        {
            try
            {
                bool ifExist = IfUserExist(req);
                if (ifExist)
                {
                    return new ServiceResponse(false, "An error occurred : The Phone-Number or Email is already used.");
                }
                else
                {
                    User user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Username = req.Username,
                        Password = req.Password,
                        Email = req.Email,
                        Telephone = req.Telephone
                    };
                    await _users.InsertOneAsync(user);
                    string token = GetAuthenticated(user);
                    return token == string.Empty ? new ServiceResponse(false, "An error occurred : Get No Token.") : new ServiceResponse(true, token);
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }
        }

        public bool IfUserExist(SignUpRequest req)
        {
            try
            {
                var userIn = _users.Find<User>(user => user.Telephone == req.Telephone || user.Email == req.Email).FirstOrDefault();
                return (userIn == null) ? false : true;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }

        }

        public ServiceResponse Update(string id, User userIn)
        {
            try
            {
                _users.ReplaceOne(book => book.Id == id, userIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }
        }
        public async Task<ServiceResponse> UpdateAsync(string id, User userIn)
        {
            try
            {
                await _users.ReplaceOneAsync(book => book.Id == id, userIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }
        }

        public string GetAuthenticated(User user)
        {
            string token = string.Empty;
            try
            {
                var claims = new[]{
                    new Claim("id",user.Id),
                    new Claim("username",user.Username),
                    new Claim("tel",user.Telephone),
                    new Claim("email",user.Email)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtToken = new JwtSecurityToken(_token.Issuer, _token.Audience, claims, expires: DateTime.Now.AddMinutes(_token.AccessExpiration), signingCredentials: credentials);
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
            return token;
        }
    }
}