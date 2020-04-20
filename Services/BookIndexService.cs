using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
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

        public BookIndex Get(string bookIndexId) =>
            _bookindex.Find<BookIndex>(bookIndex => bookIndex.Id == bookIndexId).FirstOrDefault();
        public IOrderedEnumerable<BookIndex> GetBookIndexs(string bookId) =>
            _bookindex.Find<BookIndex>(bookIndex => bookIndex.BookId == bookId).ToList().OrderBy(bookIndex => bookIndex.Index);
        public BookIndex GetBookIndex(string bookId, int index) =>
            _bookindex.Find<BookIndex>(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index).FirstOrDefault();

        public ServiceResponse Create(BookIndex bookIndex)
        {
            try
            {
                _bookindex.InsertOne(bookIndex);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> CreateAsync(BookIndex bookIndex)
        {
            try
            {
                await _bookindex.InsertOneAsync(bookIndex);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }

        public ServiceResponse Update(string bookIndexId, BookIndex bookIndexIn)
        {
            try
            {
                _bookindex.ReplaceOne(bookIndex => bookIndex.Id == bookIndexId, bookIndexIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }

        }
        public ServiceResponse UpdateBookIndex(string bookId, int index, BookIndex bookIndexIn)
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
        public async Task<ServiceResponse> UpdateBookIndexAsync(string bookId, int index, BookIndex bookIndexIn)
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
        public ServiceResponse Remove(string bookIndexId)
        {
            try
            {
                _bookindex.DeleteOne(bookIndex => bookIndex.Id == bookIndexId);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public ServiceResponse RemoveBookIndex(string bookId, int index)
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
        public async Task<ServiceResponse> RemoveBookIndexAsync(string bookId, int index)
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