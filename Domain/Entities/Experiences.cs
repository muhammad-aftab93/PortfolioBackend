using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Database.Entities
{
    public class Experiences
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("title")]
        public string Title { get; set; } = null!;
        [BsonElement("company")]
        public string Company { get; set; } = null!;
        [BsonElement("location")]
        public string Location { get; set; } = null!;
        [BsonElement("from")]
        public DateTime From { get; set; }
        [BsonElement("to")]
        public DateTime To { get; set; }
        [BsonElement("current")]
        public bool Current { get; set; }
    }
}
