using Microsoft.EntityFrameworkCore;
using JEX_backend.API.Entities;

namespace JEX_backend.API.Data
{
    public class JEXDbContext : DbContext
    {
        public JEXDbContext(DbContextOptions<JEXDbContext> options)
            : base(options)
        {
            Companies = Set<Company>();
            JobOpenings = Set<JobOpening>();
        }

        public DbSet<Company>? Companies { get; set; }
        public DbSet<JobOpening>? JobOpenings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(e => e.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<JobOpening>().Property(e => e.Id).HasDefaultValueSql("NEWID()");

            modelBuilder
                .Entity<JobOpening>()
                .HasOne(job => job.Company)
                .WithMany(comp => comp.JobOpenings)
                .HasForeignKey(job => job.CompanyId);
        }
    }
}
