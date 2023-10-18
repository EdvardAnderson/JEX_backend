using JEX_backend.Models;
public class CompanyService : ICompanyService
{
    private readonly JEXDbContext _context;

    public CompanyService(JEXDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetCompaniesAsync()
    {
        return null;//await _context.Companies.ToListAsync();
    }
}