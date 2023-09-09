using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using ToyStore_API.Data;
using ToyStore_API.Models.Products;

namespace Tests.IntegrationTests.Controllers
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // set up services
                });
            });
        }

        [Fact]
        public async Task ShouldGetListOfProductsProperly()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/products");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
            Assert.NotNull(content);
            Assert.IsAssignableFrom<IEnumerable<Product>>(content);
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task ShouldGetProductByIdProperly()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var allProducts = await client.GetFromJsonAsync<IEnumerable<Product>>("/api/products");
            var firstProduct = allProducts?.First();
            var response = await client.GetAsync($"/api/products/{firstProduct?.Id}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(content);
            Assert.IsAssignableFrom<Product>(content);
            firstProduct.Should().BeEquivalentTo(content);
        }

        [Fact]
        public async Task ShouldCreateNewProductsProperly()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newProductPayload = new Product
            {
                Name = "Test Product",
                Description = "A brand new testing product.",
                AgeRestriction = null,
                Company = "IntegrationTests",
                Price = 999.99m
            };
            var requestBody = new StringContent(JsonConvert.SerializeObject(newProductPayload), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/products", requestBody);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(content);
            Assert.IsAssignableFrom<Product>(content);
            newProductPayload.Should().BeEquivalentTo(content, options => options.Excluding(p => p.Id));
        }
    }

}