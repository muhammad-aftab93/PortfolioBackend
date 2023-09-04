using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Api.Models
{
    public class PersonalDetails
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalStatement { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Picture { get; set; } = null!;
    }
}
