namespace Api.Models
{
    public class CreateExperienceRequest
    {
        public string Title { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Current { get; set; }
    }
}
