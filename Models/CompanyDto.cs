namespace JEX_backend.API.Models
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<JobOpeningDto> JobOpenings { get; set; }
    }
}
