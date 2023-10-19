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
        private static Guid Guid1 = Guid.NewGuid();
        private static Guid Guid2 = Guid.NewGuid();
        private List<Company> Companies = new List<Company>{
            new Company{
                Id = Guid1,
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
                Id = Guid2,
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
            //var companies = await _companyService.GetCompaniesAsync();
            return Companies; //todo have them return from db
        }

        [HttpGet("{id}")]
        public async Task<Company> GetCompany(Guid id)
        {
            //var company = await _companyService.GetCompanyAsync(id);
            return Companies.FirstOrDefault(x=>x.Id == id);
        }

        [HttpPost]
        public async Task CreateCompany([FromBody]Company company)
        {
            await _companyService.CreateCompanyAsync(company);
        }

        


    }
}
