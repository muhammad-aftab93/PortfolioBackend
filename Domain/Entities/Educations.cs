using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Educations
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("institute")]
        public string Institute { get; set; } = null!;
        [BsonElement("degree")]
        public string Degree { get; set; } = null!;
        [BsonElement("start_date")]
        public DateOnly StartDate { get; set; }
        [BsonElement("end_date")]
        public DateOnly EndDate { get; set; }
        [BsonElement("grade")]
        public string Grade { get; set; } = null!;
        [BsonElement("description")]
        public string Description { get; set; } = null!;
    }
}
