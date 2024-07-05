using JEX_backend.API.Entities;

namespace JEX_backend.API.Services
{
    public interface ICompanyRepository : ISaveable
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(); // IQueryable? leaks persistence logic outside the repo.

        Task<Company?> GetCompanyAsync(Guid companyId, bool includeJobOpenings);
        Task<IEnumerable<JobOpening>> GetJobOpeningsPerCompanyAsync(Guid companyId);

        Task<bool> CompanyExistsAsync(Guid companyId);
        Task CreateCompanyAsync(Company companyCreated);
    }
}
