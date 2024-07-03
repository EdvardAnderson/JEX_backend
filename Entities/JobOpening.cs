using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JEX_backend.API.Entities
{
public class JobOpening
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Title { get; set; }
    
    [Required]
    [MaxLength(4000)]
    public string? Description { get; set; }
    
    public bool IsActive { get; set; }

    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
    public Guid CompanyId { get; set; }

   
}
}