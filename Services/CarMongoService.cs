using MongoDB.Driver;
using MongoDB.Bson;
using MyWebApi.Models;
using Microsoft.Extensions.Configuration;
namespace MyWebApi.Services
{
    public class CarMongoDBService
    {
        private readonly IMongoCollection<Car> cars;
        public CarMongoDBService(CarDatabaseSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            cars = databases.GetCollection<Car>(config.CollectionName);
        }
    }

}