using Microsoft.EntityFrameworkCore;

namespace BabyNanny.Api.Models
{
    public class BabyNannyContext(DbContextOptions<BabyNannyContext> options) : DbContext(options)
    {
        public DbSet<Child> Children => Set<Child>();
        public DbSet<BabyAction> Actions => Set<BabyAction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Child>()
                .HasMany(c => c.Actions)
                .WithOne(a => a.Child)
                .HasForeignKey(a => a.ChildId);
        }
    }
}
