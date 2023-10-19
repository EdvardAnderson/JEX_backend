using JEX_backend.Models;
using Microsoft.EntityFrameworkCore;
public class CompanyService : ICompanyService
{
    private readonly JEXDbContext _context;

    public CompanyService(JEXDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetCompaniesAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company> GetCompanyAsync(Guid id)
    {
        return await _context.Companies.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task CreateCompanyAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
    }

}