namespace DDDExample.Infra.Data.Context
{
    using DDDExample.Domain.Configs;
    using Domain.Entities;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        private readonly DatabaseConfigurations _databaseConfigurations;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, DatabaseConfigurations databaseConfigurations) : base(options)
        {
            _databaseConfigurations = databaseConfigurations;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}