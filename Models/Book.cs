using System;
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
        public List<Index> Index { get; set; }
        public string Link { get; set; }
    }

    public partial class BookIndex
    {
        public string BookId { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
    }
    public partial class BookContent
    {
        public string BookId { get; set; }
        public int Index { get; set; }
        public string Content { get; set; }
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
