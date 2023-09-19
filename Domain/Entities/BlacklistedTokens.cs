using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class BlacklistedTokens
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("token")]
        public string Token { get; set; } = null!;
        [BsonElement("blacklisted_on")]
        public DateTime BlacklistedOn { get; set; }
    }
}
