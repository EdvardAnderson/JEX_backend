using JEX_backend.Models;
public interface ICompanyService
{
    Task<List<Company>> GetCompaniesAsync();

    Task<Company> GetCompanyAsync(Guid id);

    Task CreateCompanyAsync(Company company);
}