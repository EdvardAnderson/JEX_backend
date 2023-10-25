using JEX_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JEX_backend.Controllers
{
    [ApiController]
    [Route("api/companies")]
    [JsonConverter(typeof(NoValuesJsonConverter))]
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
        public async Task<ActionResult<List<CompanyDto>>> GetCompaniesWithhJobOpeningsAsync()
        {
            var companies = await _companyService.GetCompaniesWithJobopeningsAsync();
            return GetCustomizedCompanyDtoList(companies);
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetCompaniesAsync()
        {
            var companies = await _companyService.GetCompaniesAsync();
            return GetCustomizedCompanyDtoList(companies);
        }

        [HttpGet("{id}")]
        public async Task<CompanyDto> GetCompany(Guid id)
        {
            var companies = await _companyService.GetCompaniesAsync();
            
            var company = GetCustomizedCompanyDtoList(companies).FirstOrDefault(x => x.Id == id);
            if (company == null)
            {
                HttpContext.Response.StatusCode = 404;
            }
            return await Task.FromResult<CompanyDto>(company);
        }

        [HttpPost]
        public async Task CreateCompany([FromBody] Company company)
        {
            if (
                string.IsNullOrWhiteSpace(company.Name)
                || string.IsNullOrWhiteSpace(company.Address)
            )
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

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<Company> UpdateCompany([FromBody] Company company)
        {
            var updatedCompany = await _companyService.UpdateAsync(company.Id, company);
            return await Task.FromResult(updatedCompany);
        }


        [HttpDelete("{id}")]
        public async Task DeleteCompany(Guid id)
        {
            await _companyService.DeleteAsync(id);
        }

        private List<CompanyDto> GetCustomizedCompanyDtoList(List<Company> companies)
        {
            if(companies == null) return new List<CompanyDto>();
             var companyDtos = companies
                .Select(
                    company =>
                        new CompanyDto
                        {
                            Id = company.Id,
                            Name = company.Name,
                            Address = company.Address,
                            JobOpenings = company.JobOpenings
                                .Select(
                                    job =>
                                        new JobOpeningDto
                                        {
                                            Id = job.Id,
                                            Title = job.Title,
                                            Description = job.Description,
                                            IsActive = job.IsActive
                                        }
                                )
                                .ToList()
                        }
                )
                .ToList();

            return companyDtos;
        }
    }
}
