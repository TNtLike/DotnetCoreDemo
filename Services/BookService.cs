using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
namespace MyWebApi.Services
{
    public class BookService : IBaseService<Book>
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IMongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _books = databases.GetCollection<Book>(nameof(Book));
        }
        public List<Book> GetTs() =>
            _books.Find<Book>(Book => true).ToList();

        public Book GetT(string id) =>
            _books.Find<Book>(Book => Book.Id == id).FirstOrDefault();

        public BaseService Create(Book Book)
        {
            try
            {
                _books.InsertOne(Book);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public async Task<BaseService> CreateAsync(Book Book)
        {
            try
            {
                await _books.InsertOneAsync(Book);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public BaseService Update(string id, Book BookIn)
        {
            try
            {
                _books.ReplaceOne(Book => Book.Id == id, BookIn);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public async Task<BaseService> UpdateAsync(string id, Book BookIn)
        {
            try
            {
                await _books.ReplaceOneAsync(Book => Book.Id == id, BookIn);
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
                _books.DeleteOne(Book => Book.Id == id);
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
                await _books.DeleteOneAsync(Book => Book.Id == id);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
    }
}