using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyWebApi.Models
{
    public interface ICarDBSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class CarDBSettings : ICarDBSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Brand { get; set; }
        public string CarTypeName { get; set; }

        public string CarClass { get; set; }
        public int Price { get; set; }
        public DateTime ProdDate { get; set; }
        public int MaxSpeed { get; set; }
    }
}
