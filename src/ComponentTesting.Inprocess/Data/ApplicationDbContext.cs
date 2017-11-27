using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComponentTesting.Inprocess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityType = typeof(Entity);
            entityType
                .Assembly
                .GetExportedTypes()
                .Where(type => entityType.IsAssignableFrom(type))
                .ToList()
                .ForEach(etype => modelBuilder.Entity(etype));
        }
    }
}