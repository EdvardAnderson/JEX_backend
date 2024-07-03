

using JEX_backend.API.Entities;

namespace JEX_backend.API.Services
{

public class JobOpeningService : IJobOpeningService
{
     public Task<IEnumerable<JobOpening>> GetJobOpeningsByCompany(Guid companyId)
    {
        throw new NotImplementedException();
    }


    public Task<JobOpening> CreateJobOpeningAsync(JobOpening jobOpening, Guid companyId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteJobOpeningAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<JobOpening> GetJobOpeningById(Guid id)
    {
        throw new NotImplementedException();
    }

   
    public Task<JobOpening> UpdateJobOpeningAsync(Guid id, JobOpening jobOpening)
    {
        throw new NotImplementedException();
    }
}
}