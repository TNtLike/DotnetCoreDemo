using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MyWebApi.Models
{
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

        public string CarId { get; set; }
        public string Info { get; set; }
        public int Size { get; set; }
        public byte[] CodeImg { get; set; }
    }
}
