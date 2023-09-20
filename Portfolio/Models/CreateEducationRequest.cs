using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class CreateEducationRequest
    {
        public string Institute { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Grade { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
