using JEX_backend.API.Data;
using JEX_backend.API.Entities;
using JEX_backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JEX_backend.API.Services
{
    public class JobOpeningRepository : IJobOpeningRepository
    {
        private readonly JEXDbContext _context;

        public JobOpeningRepository(JEXDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<JobOpening>> GetJobOpeningsPerCompanyAsync(Guid companyId)
        {
            return await _context.JobOpenings.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<JobOpening> GetJobOpeningPerCompanyAsync(
            Guid companyId,
            Guid jobOpeningId
        )
        {
            return await _context.JobOpenings
                .Where(x => x.Id == jobOpeningId && x.CompanyId == companyId)
                .FirstOrDefaultAsync();
        }

        public async Task<JobOpening> GetJobOpening(Guid jobOpeningId)
        {
            return await _context.JobOpenings
                .Where(_ => _.Id == jobOpeningId)
                .FirstOrDefaultAsync();
        }

        public async Task AddJobOpeningToCompany(JobOpening jobOpening, Guid companyId)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(_ => _.Id == companyId);
            if (company == null)
            {
                throw new ArgumentNullException(
                    "Could not add a jobopening to the company",
                    nameof(company)
                );
            }
            company.JobOpenings.Add(jobOpening);
        }

        public async Task<bool> JobOpeningForCompanyExistsAsync(Guid jobOpeningId, Guid companyId)
        {
            if (!await _context.Companies.AnyAsync(_ => _.Id == companyId))
                return false;

            return await _context.JobOpenings.AnyAsync(
                _ => _.Id == jobOpeningId && _.CompanyId == companyId
            );
        }

        public async Task<bool> JobOpeningExistsAsync(Guid jobOpeningId)
        {
            return await _context.JobOpenings.AnyAsync(_ => _.Id == jobOpeningId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
