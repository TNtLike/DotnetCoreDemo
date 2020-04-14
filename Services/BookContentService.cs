using MongoDB.Driver;
using MyWebApi.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace MyWebApi.Services
{
    public class BookContentService : IBaseService<BookContent>
    {
        private readonly IMongoCollection<BookContent> _bookcontent;
        public BookContentService(IMongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _bookcontent = databases.GetCollection<BookContent>(nameof(BookContent));
        }
        public BookContent GetT(string bookContextId) =>
            _bookcontent.Find<BookContent>(content => content.Id == bookContextId).FirstOrDefault();
        public IOrderedEnumerable<BookContent> GetBookContents(string bookId) =>
            _bookcontent.Find<BookContent>(content => content.BookId == bookId).ToList().OrderBy(content => content.Index);
        public BookContent GetBookContent(string bookId, int index) =>
            _bookcontent.Find<BookContent>(bookIndex => bookIndex.BookId == bookId && bookIndex.Index == index).FirstOrDefault();
        public ServiceResponse Create(BookContent content)
        {
            try
            {
                _bookcontent.InsertOne(content);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> CreateAsync(BookContent content)
        {
            try
            {
                await _bookcontent.InsertOneAsync(content);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }

        public ServiceResponse Update(string contentId, BookContent contentIn)
        {
            try
            {
                _bookcontent.ReplaceOne(content => content.Id == contentId, contentIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }

        }
        public ServiceResponse UpdateBookIndex(string bookId, int index, BookContent contentIn)
        {
            try
            {
                _bookcontent.ReplaceOne(content => content.BookId == bookId && content.Index == index, contentIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public async Task<ServiceResponse> UpdateBookIndexAsync(string bookId, int index, BookContent contentIn)
        {
            try
            {
                await _bookcontent.ReplaceOneAsync(content => content.BookId == bookId && content.Index == index, contentIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
        public ServiceResponse Remove(string contentId)
        {
            try
            {
                _bookcontent.DeleteOne(content => content.Id == contentId);
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
                _bookcontent.DeleteOne(content => content.BookId == bookId && content.Index == index);
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
                await _bookcontent.DeleteOneAsync(content => content.BookId == bookId && content.Index == index);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse($"An error occurred : {e.Message}");
            }
        }
    }
}