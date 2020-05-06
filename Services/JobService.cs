using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Linq;
namespace MyWebApi.Services
{
    public class JobService : IJobService<Job>
    {
        private readonly IMongoCollection<Job> _jobs;
        public JobService(MongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _jobs = databases.GetCollection<Job>("51nbjoblist");
        }
        public List<Job> Gets(int currpage, int pagenum) => _jobs.Find<Job>(job => true).Limit(currpage * pagenum).Skip(pagenum * (currpage - 1)).ToList();

        public Job Get(string id) => _jobs.Find<Job>(job => true).FirstOrDefault();

        public ServiceResponse Create(Job job) => new ServiceResponse();

        public ServiceResponse Update(string id, Job jobIn) => new ServiceResponse();


    }
}