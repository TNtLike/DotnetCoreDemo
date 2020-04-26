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
        public ServiceResponse(bool Success = true, string Message = "success") : base(Success, Message) { }
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

    public class Key
    {
        public string Secret { get; set; }

    }

    public interface IQRCodeService<T>
    {
        T InitCode(string unionid, string url, int pixel);
    }
    public interface IBaseService<T>
    {
        T Get(string id);
        ServiceResponse Create(T item);
        ServiceResponse Update(string id, T item);
    }
    public interface IAuthenticateService<T>
    {
        string GetAuthenticated(T item);
    }
}