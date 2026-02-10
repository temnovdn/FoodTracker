using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodTracker.Models;

namespace FoodTracker.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<FoodTracker.Models.Food> Food { get; set; } = default!;
        public DbSet<FoodTracker.Models.ConsumedFood> ConsumedFood { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Food>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ConsumedFood>().Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}
