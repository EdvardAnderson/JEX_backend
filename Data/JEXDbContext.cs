using Microsoft.EntityFrameworkCore;
using JEX_backend.Models;

    public class JEXDbContext : DbContext
    {
        public JEXDbContext (DbContextOptions<JEXDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<JobOpening> JobOpenings { get; set; } = default!;
}
