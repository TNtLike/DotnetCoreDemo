using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MyWebApi.Models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string JobName { get; set; }
        public string JobFunc { get; set; }
        public string JobLink { get; set; }
        public string EntName { get; set; }
        public string EntLink { get; set; }
        public string AreaName { get; set; }
        public string Salary { get; set; }
        public dynamic MinSalary { get; set; }
        public dynamic MaxSalary { get; set; }
        public string Date { get; set; }
        public string InsertDate { get; set; }
    }

}
