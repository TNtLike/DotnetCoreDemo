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
        public dynamic Data { get; set; }
        public BaseResponse(bool _Success, string _Message, dynamic _Data = null)
        {
            Status = _Success ? "ok" : "error";
            Msg = _Message;
            Data = _Data;
        }
    }
    public class ServiceResponse : BaseResponse
    {
        public ServiceResponse(bool Success = true, string Message = "success", object Data = null) : base(Success, Message, Data) { }
    }
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public class TokenManagement
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessExpiration { get; set; }
        public int RefreshExpiration { get; set; }

    }

    public class OriginsWhiteList
    {
        public string Origin { get; set;}

    }

    public interface IQRCodeService<T>
    {
        T InitCode(string unionid, string url, int pixel);
    }
    public interface IAuthenticateService<T>
    {
        string GetAuthenticated(T item);
    }


    public interface IUserService<T> : IAuthenticateService<T> { }
    public interface ICodeService<T> : IQRCodeService<T> { }
    public interface IJobService<T> { }
    public interface IFileService<T> { }


}