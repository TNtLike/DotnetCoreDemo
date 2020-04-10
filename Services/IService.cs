using System;
using System.Collections.Generic;

namespace MyWebApi.Services
{

    public abstract class BaseResponse
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public BaseResponse(bool Success, string Message)
        {
            Status = Success ? "ok" : "error";
            Msg = Message;
        }
    }
    public class BaseService : BaseResponse
    {
        private BaseService(bool Success, string Message) : base(Success, Message) { }
        public BaseService() : this(true, string.Empty) { }
        public BaseService(string Message) : this(false, Message) { }
    }
    public interface IQRCodeService<T>
    {
        T InitCode(string unionid, string url, int pixel);
    }
    public interface IBaseService<T>
    {
        List<T> GetTs();
        T GetT(string id);
        BaseService Create(T item);
        BaseService Remove(string id);
        BaseService Update(string id, T item);

    }
    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class MyDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}