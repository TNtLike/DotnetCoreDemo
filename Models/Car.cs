using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyWebApi.Models
{
    public interface IMongoDBSettings
    {
        string CarCollectionName { get; set; }
        string CodeCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class MyDBSettings : IMongoDBSettings
    {
        public string CarCollectionName { get; set; }
        public string CodeCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class Car
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string CarTypeName { get; set; }

        public string CarClass { get; set; }
        public int Price { get; set; }
        public DateTime ProdDate { get; set; }
        public int MaxSpeed { get; set; }
    }
    public class Code
    {

        public string Id { get; set; }
        public string Info { get; set; }
        public int Size { get; set; }
        public byte[] CodeImg { get; set; }
    }
}
