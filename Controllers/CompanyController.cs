using JEX_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace JEX_backend.Controllers
{

    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly JEXDbContext _context;
        private readonly ICompanyService _companyService;
        private List<Company> Companies = new List<Company>{
            new Company{
                Id = Guid.NewGuid(),
                Name = "JEX",
                Address = "Nassaukade 162",
                JobOpenings = new List<JobOpening>{
                    new JobOpening{
                        Id = Guid.NewGuid(),
                        Title = "Fullstack ontwikkelaar",
                        Description = "Fullstack lorem ipsum",
                        IsActive = true
                    },
                     new JobOpening{
                        Id = Guid.NewGuid(),
                        Title = "Backend ontwikkelaar",
                        Description = "Backend lorem ipsum",
                        IsActive = false
                    }
                }
            },
             new Company{
                Id = Guid.NewGuid(),
                Name = "JEX2",
                Address = "Nassaukade 162",
                JobOpenings = new List<JobOpening>{
                    new JobOpening{
                        Id = Guid.NewGuid(),
                        Title = "Fullstack ontwikkelaar2",
                        Description = "Fullstack lorem ipsum",
                        IsActive = true
                    },
                     new JobOpening{
                        Id = Guid.NewGuid(),
                        Title = "Backend ontwikkelaar2",
                        Description = "Backend lorem ipsum",
                        IsActive = false
                    }
                }
            }
        };

        public CompanyController(JEXDbContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetCompaniesAsync()
        {
            var companies = await _companyService.GetCompaniesAsync();
            return Companies; //todo have them return from db
        }


    }
}
