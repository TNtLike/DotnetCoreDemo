
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MyWebApi.Models
{

    public class Code
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UnionId { get; set; }
        public string Info { get; set; }
        public int Size { get; set; }
        public byte[] CodeImg { get; set; }
    }
}
