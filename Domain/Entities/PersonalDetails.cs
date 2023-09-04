using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class PersonalDetails
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("user_id")]
        public string UserId { get; set; } = null!;
        [BsonElement("first_name")]
        public string FirstName { get; set; } = null!;
        [BsonElement("middle_name")]
        public string MiddleName { get; set; } = null!;
        [BsonElement("last_name")]
        public string LastName { get; set; } = null!;
        [BsonElement("personal_statement")]
        public string PersonalStatement { get; set; } = null!;
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; } = null!;
        [BsonElement("location")]
        public string Location { get; set; } = null!;
        [BsonElement("picture")]
        public string Picture { get; set; } = null!;
    }
}
