using JEX_backend.Models;
public interface ICompanyService
{
    Task<List<Company>> GetCompaniesAsync();
}