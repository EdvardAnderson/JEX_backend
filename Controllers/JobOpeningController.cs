using JEX_backend.API.Models;
using JEX_backend.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JEX_backend.API.Controllers
{
    [Route("api/companies/{companyId}/jobOpenings")]
    [ApiController]
    public class JobOpeningController : ControllerBase
    {
        private readonly IJobOpeningService _jobOpeningService;

        public JobOpeningController(IJobOpeningService jobOpeningService)
        {
            _jobOpeningService = jobOpeningService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOpeningDto>>> GetJobOpenings(Guid companyId)
        {
            return Ok();
        }
    }
}
