using System.Collections.Generic;

namespace MyWebApi.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

    }

    public partial class Customer
    {
        public List<Book> SaveBookList { get; set; }
        public List<Book> LikeBookList { get; set; }
        public List<Book> OwnBookList { get; set; }
    }

    public partial class Merchant
    {
        public List<Book> PubBookList { get; set; }
        public List<Book> SoldBookList { get; set; }
        public List<Book> OwnBookList { get; set; }
    }

}