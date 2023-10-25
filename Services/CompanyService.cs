using System.Security.Cryptography.X509Certificates;
using JEX_backend.Models;
using Microsoft.EntityFrameworkCore;
public class CompanyService : ICompanyService
{
    private readonly JEXDbContext _context;

    public CompanyService(JEXDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Company>> GetCompaniesWithJobopeningsAsync()
    {
        var jobOpenings = await _context.JobOpenings.ToListAsync();
        var companies = await _context.Companies.ToListAsync();
        var companiesWithJobOpenings = companies.GroupJoin(
            jobOpenings.Where(job=>job.IsActive), //only active jobs
            company => company.Id, 
            jobOpening => jobOpening.CompanyId, // FK
            (company, openings) => new Company
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                JobOpenings = openings.ToList()
            }
        ).Where(company=>company.JobOpenings.Any());
       
        return await Task.FromResult(companiesWithJobOpenings.ToList());
    }

    public async Task<List<Company>> GetCompaniesAsync()
    {
        var jobOpenings = await _context.JobOpenings.ToListAsync();
        var companies = await _context.Companies.ToListAsync();
        var companiesWithJobOpenings = companies.GroupJoin(
            jobOpenings,
            company => company.Id, 
            jobOpening => jobOpening.CompanyId, // FK
            (company, openings) => new Company
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                JobOpenings = openings.ToList()
            }
        );
        return  await Task.FromResult(companiesWithJobOpenings.ToList());
    }


    public async Task<Company> GetCompanyAsync(Guid id)
    {

        return await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Company> CreateCompanyAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> UpdateAsync(Guid id, Company company)
    {
        var existingCompany = await _context.Companies.FindAsync(id);
        if (existingCompany == null)
        {
            return null;
        }

        // Update the properties of the existing entity
        _context.Entry(existingCompany).CurrentValues.SetValues(company);

        await _context.SaveChangesAsync();
        return existingCompany;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var companyForDeletion = await _context.Companies.FindAsync(id);
        if (companyForDeletion == null)
            return false;

        _context.Companies.Remove(companyForDeletion);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<JobOpening> CreateJobOpeningAsync(JobOpening jobOpening)
    {
        // Update the properties of the existing entity
        var newJobOpening = new JobOpening
        {
            Title = jobOpening.Title,
            Description = jobOpening.Description,
            CompanyId = jobOpening.CompanyId,
            IsActive = jobOpening.IsActive
        };
        _context.JobOpenings.Add(newJobOpening);

        await _context.SaveChangesAsync();
        return newJobOpening;
    }

    public async Task<JobOpening> UpdateJobOpeningAsync(Guid id, JobOpening jobOpening)
    {
         var existingJobOpening = await _context.JobOpenings.FindAsync(id);
        if (existingJobOpening == null)
        {
            return null;
        }

        // Update the properties of the existing entity
        _context.Entry(existingJobOpening).CurrentValues.SetValues(jobOpening);

        await _context.SaveChangesAsync();
        return existingJobOpening;
    }

    public async Task<bool> DeleteJobOpeningAsync(Guid id)
    {
         var jobopeningForDeletion = await _context.JobOpenings.FindAsync(id);
        if (jobopeningForDeletion == null)
            return false;

        _context.JobOpenings.Remove(jobopeningForDeletion);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<JobOpening> GetJobOpeningById(Guid id)
    {
        return await _context.JobOpenings.FirstOrDefaultAsync(x=>x.Id == id);
    }
}