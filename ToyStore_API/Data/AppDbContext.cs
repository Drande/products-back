using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToyStore_API.Data.Seed;
using ToyStore_API.Models.Products;

namespace ToyStore_API.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }


        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasData(ProductsSeeds.Sample1);
        }
    }
}
