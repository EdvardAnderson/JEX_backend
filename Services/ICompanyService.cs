using JEX_backend.Models;
public interface ICompanyService
{
    Task<List<Company>> GetCompaniesAsync();

    Task<Company> GetCompanyAsync(Guid id);

    Task<Company> CreateCompanyAsync(Company company);

    Task<Company> UpdateAsync(Guid id, Company company);

    Task<bool> DeleteAsync(Guid id);

    Task<JobOpening> CreateJobOpeningAsync(JobOpening jobOpening);
}