using System;
using System.Collections.Generic;
using MongoDB.Driver;

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
    public class ServiceResponse : BaseResponse
    {
        private ServiceResponse(bool Success, string Message) : base(Success, Message) { }
        public ServiceResponse() : this(true, string.Empty) { }
        public ServiceResponse(string Message) : this(false, Message) { }
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
    public interface IQRCodeService<T>
    {
        T InitCode(string unionid, string url, int pixel);
    }
    public interface IBaseService<T>
    {
        List<T> GetTs();
        T GetT(string id);
        ServiceResponse Create(T item);
        ServiceResponse Remove(string id);
        ServiceResponse Update(string id, T item);
    }
}