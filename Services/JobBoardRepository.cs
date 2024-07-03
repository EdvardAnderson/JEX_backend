using JEX_backend.API.Data;
using JEX_backend.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace JEX_backend.API.Services
{
    public class JobBoardRepository : IJobBoardRepository
    {
        private readonly JEXDbContext _context;

        public JobBoardRepository(JEXDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.OrderBy(_ => _.Name).ToListAsync();
        }

        public async Task<Company?> GetCompanyAsync(Guid companyId, bool includeJobOpenings)
        {
            return includeJobOpenings
                ? await _context.Companies
                    .Include(x => x.JobOpenings)
                    .Where(x => x.Id == companyId)
                    .FirstOrDefaultAsync()
                : await _context.Companies.Where(x => x.Id == companyId).FirstOrDefaultAsync();
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
    }
}
