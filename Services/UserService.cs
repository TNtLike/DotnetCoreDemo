using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
namespace MyWebApi.Services
{
    public class UserService : IBaseService<User>
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IMongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _users = databases.GetCollection<User>(nameof(User));
        }

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();


        public ServiceResponse Create(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Id))
                {
                    return new ServiceResponse($"An error occurred : Error Data");
                }
                _users.InsertOne(user);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> CreateAsync(SignUpRequest req)
        {
            try
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
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public ServiceResponse Update(string id, User BookIn)
        {
            try
            {
                _users.ReplaceOne(book => book.Id == id, BookIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> UpdateAsync(string id, User BookIn)
        {
            try
            {
                await _users.ReplaceOneAsync(book => book.Id == id, BookIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public ServiceResponse Remove(string id)
        {
            try
            {
                _users.DeleteOne(book => book.Id == id);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> RemoveAsync(string id)
        {
            try
            {
                await _users.DeleteOneAsync(book => book.Id == id);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
    }
}