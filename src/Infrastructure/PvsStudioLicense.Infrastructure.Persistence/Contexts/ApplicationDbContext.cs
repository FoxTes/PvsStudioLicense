namespace PvsStudioLicense.Infrastructure.Persistence.Contexts
{
    using Domain.Entities;
    using EntityFramework.Exceptions.Sqlite;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public class ApplicationDbContext : DbContext
    {
        /// <inheritdoc />
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Products.
        /// </summary>
        public DbSet<Project> Products { get; set; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }
    }
}