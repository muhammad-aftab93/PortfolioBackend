using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Entities;

public class Certifications
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("short_name")]
    public string ShortName { get; set; } = null!;
    [BsonElement("full_name")]
    public string FullName { get; set; } = null!;
}