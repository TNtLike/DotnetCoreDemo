using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
namespace MyWebApi.Services
{
    public class BookIndexService : IBaseService<BookIndex>
    {
        private readonly IMongoCollection<BookIndex> _bookindex;
        public BookIndexService(IMongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _bookindex = databases.GetCollection<BookIndex>(nameof(BookIndex));
        }
        public List<BookIndex> GetTs(string bookId) =>
            _bookindex.Find<BookIndex>(bookIndex => bookIndex.BookId == bookId).ToList();

        public BookIndex GetT(string bookId, int index) =>
            _bookindex.Find<BookIndex>(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index).FirstOrDefault();


        public BaseService Create(BookIndex BookIndex)
        {
            try
            {
                _bookindex.InsertOne(BookIndex);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public async Task<BaseService> CreateAsync(BookIndex BookIndex)
        {
            try
            {
                await _bookindex.InsertOneAsync(BookIndex);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public BaseService Update(string bookId, int index, BookIndex BookIn)
        {
            try
            {
                _bookindex.ReplaceOne(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index, BookIn);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public async Task<BaseService> UpdateAsync(string id, BookIndex BookIn)
        {
            try
            {
                await _bookindex.ReplaceOneAsync(BookIndex => BookIndex.Id == id, BookIn);
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
                _bookindex.DeleteOne(BookIndex => BookIndex.Id == id);
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
                await _bookindex.DeleteOneAsync(BookIndex => BookIndex.Id == id);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
    }
}