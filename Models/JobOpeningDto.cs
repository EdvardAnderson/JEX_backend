namespace JEX_backend.API.Models;

public class JobOpeningDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
