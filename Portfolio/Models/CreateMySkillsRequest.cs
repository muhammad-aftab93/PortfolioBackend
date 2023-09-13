namespace Api.Models;

public class CreateMySkillsRequest
{
    public string SkillName { get; set; } = null!;
    public int Proficiency { get; set; }
}