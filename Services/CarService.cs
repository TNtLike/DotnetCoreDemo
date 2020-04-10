using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
namespace MyWebApi.Services
{
    public class CarService : IBaseService<Car>
    {
        private readonly IMongoCollection<Car> _cars;
        public CarService(IMongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _cars = databases.GetCollection<Car>(nameof(Car));
        }
        public List<Car> GetTs() =>
            _cars.Find<Car>(car => true).ToList();

        public Car GetT(string id) =>
            _cars.Find<Car>(car => car.Id == id).FirstOrDefault();

        public BaseService Create(Car car)
        {
            try
            {
                _cars.InsertOne(car);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public async Task<BaseService> CreateAsync(Car car)
        {
            try
            {
                await _cars.InsertOneAsync(car);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public BaseService Update(string id, Car carIn)
        {
            try
            {
                _cars.ReplaceOne(car => car.Id == id, carIn);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public async Task<BaseService> UpdateAsync(string id, Car carIn)
        {
            try
            {
                await _cars.ReplaceOneAsync(car => car.Id == id, carIn);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public BaseService Remove(string id)
        {
            try
            {
                _cars.DeleteOne(car => car.Id == id);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public async Task<BaseService> RemoveAsync(string id)
        {
            try
            {
                await _cars.DeleteOneAsync(car => car.Id == id);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
    }
}