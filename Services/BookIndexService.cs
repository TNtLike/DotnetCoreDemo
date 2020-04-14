using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
namespace MyWebApi.Services
{
    public class BookIndexService
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

        public ServiceResponse Create(BookIndex BookIndex)
        {
            try
            {
                _bookindex.InsertOne(BookIndex);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> CreateAsync(BookIndex BookIndex)
        {
            try
            {
                await _bookindex.InsertOneAsync(BookIndex);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public ServiceResponse Update(string bookId, int index, BookIndex bookIndexIn)
        {
            try
            {
                _bookindex.ReplaceOne(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index, bookIndexIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> UpdateAsync(string bookId, int index, BookIndex bookIndexIn)
        {
            try
            {
                await _bookindex.ReplaceOneAsync(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index, bookIndexIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public ServiceResponse Remove(string bookId, int index)
        {
            try
            {
                _bookindex.DeleteOne(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> RemoveAsync(string bookId, int index)
        {
            try
            {
                await _bookindex.DeleteOneAsync(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
    }
}