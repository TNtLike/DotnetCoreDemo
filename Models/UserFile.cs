using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MyWebApi.Models
{

    public class UserFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Key { get; set; }
        public string Tag { get; set; }
        public string Index { get; set; }
        public byte[] ByteFile { get; set; }
    }
}
