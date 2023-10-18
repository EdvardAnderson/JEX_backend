using Microsoft.AspNetCore.Mvc;

namespace JEX_backend.Controllers
{
    
    [ApiController]
    [Route("[api/backoffice]")]
    public class CompanyController : ControllerBase
    {
        private readonly JEXDbContext _context;

        public CompanyController(JEXDbContext context)
        {
            _context = context;
        }

       
    }
}
