using JEX_backend.API.Entities;

namespace JEX_backend.API.Services
{
    public interface IJobOpeningRepository : ISaveable
    {
        Task<JobOpening> GetJobOpeningPerCompanyAsync(Guid companyId, Guid jobOpeningId);
        Task<JobOpening> GetJobOpening(Guid jobOpeningId);
        Task<bool> JobOpeningForCompanyExistsAsync(Guid jobOpeningId, Guid companyId);
        Task<bool> JobOpeningExistsAsync(Guid jobOpeningId);
        Task AddJobOpeningToCompany(JobOpening jobOpening, Guid companyId);
    }
}
