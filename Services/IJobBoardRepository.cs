using JEX_backend.API.Entities;

namespace JEX_backend.API.Services
{
    public interface IJobBoardRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(); // IQueryable? leaks persistence logic outside the repo.

        Task<Company?> GetCompanyAsync(Guid companyId, bool includeJobOpenings);

        Task<IEnumerable<JobOpening>> GetJobOpeningsPerCompanyAsync(Guid compannyId);
        Task<JobOpening> GetJobOpeningPerCompanyAsync(Guid companyId, Guid jobOpeningId);
    }
}
