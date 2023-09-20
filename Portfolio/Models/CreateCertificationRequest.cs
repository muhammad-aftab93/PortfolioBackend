namespace Api.Models;

public class CreateCertificationRequest
{
    public string ShortName { get; set; } = null!;
    public string FullName { get; set; } = null!;
}