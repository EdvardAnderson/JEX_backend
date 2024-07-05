using JEX_backend.API.Data;
using JEX_backend.API.Entities;
using JEX_backend.API.Services;
using Microsoft.EntityFrameworkCore;

namespace JEX_backend.API;

public class CompanyRepository : ICompanyRepository
{
    private JEXDbContext _context;

    public CompanyRepository(JEXDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync()
    {
        return await _context.Companies
            .Include(x => x.JobOpenings)
            .OrderBy(_ => _.Name)
            .ToListAsync();
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

    public async Task<bool> CompanyExistsAsync(Guid companyId)
    {
        return await _context.Companies.AnyAsync(_ => _.Id == companyId);
    }

    public async Task CreateCompanyAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
