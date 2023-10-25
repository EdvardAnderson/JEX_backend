public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<JobOpeningDto> JobOpenings { get; set; }
}