using CarSelling.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSellingWeb.Data
{
    public class CarSellingDbContext : DbContext
    {
        public CarSellingDbContext()
        {

        }

        public CarSellingDbContext(DbContextOptions options)
            : base(options)
        {
            
        }


        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
