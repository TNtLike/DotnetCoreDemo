using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MyWebApi.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Price { get; set; }
        public DateTime ProdDate { get; set; }
        public int ProdName { get; set; }
        public List<BookIndex> Index { get; set; }
        public string Link { get; set; }
    }

    public class BookIndex
    {
        public string Name { get; set; }
        public int Index { get; set; }
    }
    public class BookIn
    {
        public int Index { get; set; }
        public string Info { get; set; }
    }
    public class Code
    {
        public string Id { get; set; }
        public string UnionId { get; set; }
        public string Info { get; set; }
        public int Size { get; set; }
        public byte[] CodeImg { get; set; }
    }
}
