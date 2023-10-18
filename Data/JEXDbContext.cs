using Microsoft.EntityFrameworkCore;
using JEX_backend.Models;

    public class     JEXDbContext : DbContext
    {
        public JEXDbContext (DbContextOptions<JEXDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Company { get; set; } = default!;
    }
