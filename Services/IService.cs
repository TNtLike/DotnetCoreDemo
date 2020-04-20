using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace MyWebApi.Services
{
    public class SignInRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SignUpRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }

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
        T Get(string id);
        ServiceResponse Create(T item);
        ServiceResponse Remove(string id);
        ServiceResponse Update(string id, T item);
    }
}