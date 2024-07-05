using AutoMapper;
using JEX_backend.API.Entities;
using JEX_backend.API.Models;
using JEX_backend.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JEX_backend.API.Controllers
{
    [Route("api/companies/{companyId}/jobOpenings")]
    [ApiController]
    public class JobOpeningController : ControllerBase
    {
        private readonly IJobOpeningRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public JobOpeningController(
            ICompanyRepository companyRepository,
            IJobOpeningRepository jobRepository,
            IMapper mapper
        )
        {
            _companyRepository =
                companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _jobRepository =
                jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOpeningDto>>> GetJobOpeningsAsync(
            Guid companyId
        )
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var jobOpenings = await _companyRepository.GetJobOpeningsPerCompanyAsync(companyId);
            return Ok(_mapper.Map<IEnumerable<JobOpeningDto>>(jobOpenings));
        }

        [HttpPost]
        public async Task<ActionResult> AddJobOpeningToCompanyAsync(
            JobOpeningForCreationDto jobOpeningFor,
            Guid companyId
        )
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var jobOpeningEntity = _mapper.Map<JobOpening>(jobOpeningFor);
            await _jobRepository.AddJobOpeningToCompany(jobOpeningEntity, companyId);
            await _jobRepository.SaveChangesAsync();

            var created = _mapper.Map<JobOpeningDto>(jobOpeningEntity);
            return Ok(created);
            // return CreatedAtRoute(
            //     nameof(GetJobOpeningAsync),
            //     new { jobOpeningId = jobOpeningEntity.Id },
            //     created
            //);
        }

        [HttpGet("{jobOpeningId}", Name = "GetJobOpeningAsync")]
        public async Task<ActionResult<JobOpening>> GetJobOpeningAsync(Guid jobOpeningId)
        {
            if (!await _jobRepository.JobOpeningExistsAsync(jobOpeningId))
            {
                return BadRequest();
            }

            var entitity = await _jobRepository.GetJobOpening(jobOpeningId);
            return Ok(_mapper.Map<JobOpeningDto>(entitity));
        }
    }
}
