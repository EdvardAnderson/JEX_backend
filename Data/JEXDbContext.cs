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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Company>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<JobOpening>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<JobOpening>()
            .HasOne(job=>job.Company)
            .WithMany(comp => comp.JobOpenings)
            .HasForeignKey(job => job.CompanyId);
    }
}
