using JEX_backend.API.Entities;

namespace JEX_backend.API.Services
{
public interface IJobOpeningService
{
    
    Task<IEnumerable<JobOpening>> GetJobOpeningsByCompany(Guid companyId);

    /// <summary>
    /// Add a jobopening to a company
    /// </summary>
    /// <param name="jobOpening"></param>
    /// <param name="companyId"></param>
    /// <returns></returns>
    Task<JobOpening> CreateJobOpeningAsync(JobOpening jobOpening, Guid companyId);
    Task<JobOpening> UpdateJobOpeningAsync(Guid id, JobOpening jobOpening);
    Task<bool> DeleteJobOpeningAsync(Guid id);

    Task<JobOpening> GetJobOpeningById(Guid id);
}
}