using EPSLTServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPSLTTaskServer.Infrastructure.EntityFramework
{
    public class DiscountDbContext : DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options)
            : base(options) { }

        public DbSet<DiscountCode> DiscountCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscountDbContext).Assembly);
        }
    }
}
