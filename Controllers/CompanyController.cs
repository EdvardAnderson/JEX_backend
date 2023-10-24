using JEX_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace JEX_backend.Controllers
{

    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly JEXDbContext _context;
        private readonly ICompanyService _companyService;

        public CompanyController(JEXDbContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        [HttpGet("jobs")]
        public async Task<ActionResult<List<Company>>> GetCompaniesWithhJobOpeningsAsync()
        {
            return await _companyService.GetCompaniesWithJobopeningsAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetCompaniesAsync()
        {
            return await _companyService.GetCompaniesAsync();
        }

        [HttpGet("{id}")]
        public async Task<Company> GetCompany(Guid id)
        {
            var companiesWithJobs = await _companyService.GetCompaniesWithJobopeningsAsync();
            var company = companiesWithJobs.FirstOrDefault(x => x.Id == id);
            if (company == null)
            {
                HttpContext.Response.StatusCode = 404; 
            }
            return await Task.FromResult<Company>(company);
        }

        [HttpPost]
        public async Task CreateCompany([FromBody] Company company)
        {
            if (string.IsNullOrWhiteSpace(company.Name) || string.IsNullOrWhiteSpace(company.Address))
            {
                return;
            }

            await _companyService.CreateCompanyAsync(company);
        }

        [HttpPost]
        [Route("jobs")]
        public async Task CreateJobOpening([FromBody] JobOpening jobOpening)
        {
            await _companyService.CreateJobOpeningAsync(jobOpening);
        }
    }
}
