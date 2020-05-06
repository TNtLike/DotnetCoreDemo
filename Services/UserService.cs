using MongoDB.Driver;
using MongoDB.Bson;
using MyWebApi.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MyWebApi.Services
{

    public class UserService : IUserService<Account>
    {
        private readonly IMongoCollection<Account> _acc;
        private readonly IMongoCollection<User> _users;
        private readonly TokenManagement _token;
        public UserService(MongoDBSettings config, TokenManagement token)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _acc = databases.GetCollection<Account>(nameof(Account));
            _users = databases.GetCollection<User>(nameof(User));
            _token = token;
        }

        public async Task<ServiceResponse> GetAccAsync(SignInRequest req)
        {
            try
            {
                Account accIn = await _acc.Find<Account>(Account => Account.Username == req.Username || Account.Telephone == req.Username || Account.Email == req.Username).FirstOrDefaultAsync();
                if (accIn == null)
                {
                    return new ServiceResponse(false, "An error occurred : No Such Account.");
                }
                else
                {
                    if (accIn.Password == req.Password)
                    {
                        string token = GetAuthenticated(accIn);
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

        public async Task<ServiceResponse> CreateAsync(SignUpRequest req)
        {
            try
            {
                bool ifExist = IfAccountExist(req);
                if (ifExist)
                {
                    return new ServiceResponse(false, "An error occurred : The Phone-Number or Email is already used.");
                }
                else
                {
                    Account Account = new Account
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        Username = req.Username,
                        Password = req.Password,
                        Email = req.Email,
                        Telephone = req.Telephone
                    };
                    await _acc.InsertOneAsync(Account);
                    string token = GetAuthenticated(Account);
                    return token == string.Empty ? new ServiceResponse(false, "An error occurred : Get No Token.") : new ServiceResponse(true, token);
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }
        }


        public async Task<ServiceResponse> UpdateAsync(string id, Account accIn)
        {
            try
            {
                await _acc.ReplaceOneAsync(Account => Account.Id == id, accIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }
        }

        public string GetAuthenticated(Account Account)
        {
            string token = string.Empty;
            try
            {
                var claims = new[]{
                    new Claim(ClaimTypes.Name,Account.Username),
                    new Claim(ClaimTypes.MobilePhone,Account.Telephone),
                    new Claim(ClaimTypes.Email,Account.Email)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtToken = new JwtSecurityToken(_token.Issuer, _token.Audience, claims, expires: DateTime.Now.AddHours(_token.AccessExpiration), signingCredentials: credentials);
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
            return token;
        }

        public bool IfAccountExist(SignUpRequest req)
        {
            try
            {
                var userIn = _acc.Find<Account>(Account => Account.Telephone == req.Telephone || Account.Email == req.Email).FirstOrDefault();
                return (userIn == null) ? false : true;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }


    }
}