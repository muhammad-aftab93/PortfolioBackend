namespace Api.Models
{
    public class Experiences
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Current { get; set; }
    }
}
