namespace MyWebApi.Services
{
    public class MongoService
    {
        private readonly string _dbconnect;
        public MongoService(string connect)
        {
            _dbconnect = connect;

        }

    }

}