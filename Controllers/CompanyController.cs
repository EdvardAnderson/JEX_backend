using AutoMapper;
using JEX_backend.API.Models;
using JEX_backend.API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JEX_backend.API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    [JsonConverter(typeof(NoValuesJsonConverter))]
    public class CompanyController : ControllerBase
    {
        //private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public CompanyController(
            ICompanyRepository repository,
            IMapper mapper,
            ILogger<CompanyController> logger
        )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompaniesAsync()
        {
            var companyEntities = await _repository.GetCompaniesAsync();
            return Ok(_mapper.Map<IEnumerable<CompanyDto>>(companyEntities));
        }

        [HttpGet("{companyId}", Name = "GetCompanyAsync")]
        public async Task<ActionResult> GetCompanyAsync(
            Guid companyId,
            bool includeJobOpenings = false
        )
        {
            var company = await _repository.GetCompanyAsync(companyId, includeJobOpenings);
            if (company == null)
            {
                return NotFound();
            }

            return includeJobOpenings
                ? Ok(_mapper.Map<CompanyDto>(company))
                : Ok(_mapper.Map<CompanyWithoutJobOpeningsDto>(company));
        }

        [HttpPost(Name = "CreateCompany")]
        public async Task<ActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var incoming = _mapper.Map<Entities.Company>(company);

            await _repository.CreateCompanyAsync(incoming);
            await _repository.SaveChangesAsync();

            var path = HttpContext.Request.Path;
            var method = HttpContext.Request.Method;

            var companyCreated = _mapper.Map<CompanyDto>(incoming);
            _logger.LogInformation("Request Path: {Path}", path);
            _logger.LogInformation("Request Method: {Method}", method);
            _logger.LogDebug($"Company was created: {JObject.FromObject(companyCreated)}");
            return CreatedAtRoute(
                nameof(GetCompanyAsync),
                new { companyId = companyCreated.Id },
                companyCreated
            );
        }

        /*
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
            if (companies == null)
                return new List<CompanyDto>();
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
    }*/
    }
}
