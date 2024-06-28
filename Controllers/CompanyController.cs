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
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(JEXDbContext context, ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _context = context;
            _companyService = companyService;
            _logger = logger;
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
            var path = HttpContext.Request.Path;
            var method = HttpContext.Request.Method;

            _logger.LogInformation("Request Path: {Path}", path);
            _logger.LogInformation("Request Method: {Method}", method);
            _logger.LogDebug($"Company was created: {JObject.FromObject(company)}");
        }

        [HttpPost]
        [Route("jobs")]
        public async Task CreateJobOpening([FromBody] JobOpening jobOpening)
        {
            await _companyService.CreateJobOpeningAsync(jobOpening);
            var path = HttpContext.Request.Path;
            var method = HttpContext.Request.Method;

            _logger.LogInformation("Request Path: {Path}", path);
            _logger.LogInformation("Request Method: {Method}", method);
            _logger.LogDebug($"Jobopening was created: {JObject.FromObject(jobOpening)}");
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<Company> UpdateCompany(Guid id, [FromBody] Company company)
        {
            var updatedCompany = await _companyService.UpdateAsync(id, company);
            return await Task.FromResult(updatedCompany);
        }


        [HttpDelete("{id}")]
        public async Task DeleteCompany(Guid id)
        {
            await _companyService.DeleteAsync(id);
        }



        [HttpGet]
        [Route("jobs/{id}")]
        public async Task<JobOpening> GetJobOpeningById(Guid id)
        {
            var jobOpening = await _companyService.GetJobOpeningById(id);
            return await Task.FromResult(jobOpening);
        }

        [HttpPut]
        [Route("jobopenings/{id}")]
        public async Task<JobOpening> UpdateJobOpening(Guid id, [FromBody] JobOpening jobOpening)
        {
            var updatedJobOpening = await _companyService.UpdateJobOpeningAsync(id, jobOpening);
            return await Task.FromResult(updatedJobOpening);
        }


        [HttpDelete("jobopenings/{id}")]
        public async Task DeleteJobOpening(Guid id)
        {
            await _companyService.DeleteJobOpeningAsync(id);
        }

        private List<CompanyDto> GetCustomizedCompanyDtoList(List<Company> companies)
        {
            if (companies == null) return new List<CompanyDto>();
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
