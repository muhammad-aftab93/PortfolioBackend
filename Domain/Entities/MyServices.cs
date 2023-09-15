using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Entities;

public class MyServices
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("title")]
    public string Title { get; set; } = null!;
    [BsonElement("description")]
    public string Description { get; set; } = null!;
}