using Microsoft.EntityFrameworkCore;
using ToyStore_API.Data;
using ToyStore_API.Models.Products;

namespace Tests.UnitTests.Fixtures
{
    public class DbContextProductsFixture : IDisposable
    {
        public readonly AppDbContext dbContext;
        private readonly DbContextOptions<AppDbContext> _options;

        public DbContextProductsFixture()
        {
            // Configure the DbContext options with an in-memory database
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            dbContext = new AppDbContext(_options);

            // Create and seed the database
            SeedDatabase();
        }

        public void SeedDatabase()
        {
            // Seed the database with initial data here
            AddProductsSeed();
            dbContext.SaveChanges();
        }

        public void AddProductsSeed()
        {
            var newProduct = new Product
            {
                Name = "Test Product",
                Description = "A brand new testing product from unit test suite.",
                AgeRestriction = null,
                Company = "UnitTest",
                Price = 1m
            };
            dbContext.Products.Add(newProduct);
        }

        public void Dispose()
        {
            // Dispose of resources, such as the in-memory database, if necessary
        }
    }
}
