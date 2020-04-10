using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;

namespace MyWebApi.Services
{
    public class CarService : IDBService<Car>
    {
        private readonly IMongoCollection<Car> _cars;
        public CarService(IMongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _cars = databases.GetCollection<Car>(nameof(Car));
        }
        public List<Car> Get()
        {
            var listCar = new List<Car>();
            for (var i = 0; i <= 5; i++)
            {
                listCar.Add(new Car
                {
                    Id = i.ToString(),
                    CarTypeName = "BZ"
                });
            }
            return listCar;
        }


        public Car Get(string id) =>
            _cars.Find<Car>(car => car.Id == id).FirstOrDefault();

        public Car Create(Car car)
        {
            _cars.InsertOne(car);
            return car;
        }

        public void Update(string id, Car carIn) =>
            _cars.ReplaceOne(car => car.Id == id, carIn);

        public void Remove(Car carIn) =>
            _cars.DeleteOne(car => car.Id == carIn.Id);

        public void Remove(string id) =>
            _cars.DeleteOne(car => car.Id == id);

    }

}