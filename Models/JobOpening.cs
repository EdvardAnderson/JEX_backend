using JEX_backend.Models;

public class JobOpening
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
}