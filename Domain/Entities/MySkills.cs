using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Entities;

public class MySkills
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("skill_name")]
    public string SkillName { get; set; } = null!;
    [BsonElement("proficiency")]
    public int Proficiency { get; set; }
}