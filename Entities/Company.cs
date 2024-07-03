using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEX_backend.API.Entities;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    public string? Address { get; set; }

    public ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();

    // public Company(string name)
    // {
    //     Name = name;
    // }
}
