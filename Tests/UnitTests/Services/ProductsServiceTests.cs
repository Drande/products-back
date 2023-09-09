using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tests.UnitTests.Fixtures;
using ToyStore_API.Data;
using ToyStore_API.Models.Products;
using ToyStore_API.Services;
using Xunit;

namespace Tests.UnitTests.Services
{
    public class ProductsServiceTests : IClassFixture<DbContextProductsFixture>
    {
        private readonly DbContextProductsFixture _fixture;

        public ProductsServiceTests(DbContextProductsFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public async Task GetProductListTest()
        {
            // Arrange
            var productsService = new ProductsService(_fixture.dbContext);

            // Act
            var result = await productsService.GetProductList();

            // Assert
            var existingProducts = _fixture.dbContext.Products.AsNoTracking().ToList();
            Assert.Equal(result.Count(), existingProducts.Count);
        }

        [Fact]
        public async Task GetProductByIdTest()
        {
            // Arrange
            var productsService = new ProductsService(_fixture.dbContext);

            // Get a sample product from the in-memory database
            var existingProduct = _fixture.dbContext.Products.AsNoTracking().First();

            // Act
            var result = await productsService.GetProductById(existingProduct.Id);

            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(existingProduct);
        }

        [Fact]
        public async Task CreateProductTest()
        {
            // Arrange
            var productsService = new ProductsService(_fixture.dbContext);

            // Get a sample product from the in-memory database
            var createProductPayload = new Product
            {
                Name = "New Product",
                AgeRestriction = 13,
                Description = "Create Product From Unit Test",
                Company = "Unit Test",
                Price = 1m,
            };

            // Act
            var createdProduct = await productsService.CreateProduct(createProductPayload);
            var productExists = _fixture.dbContext.Products.AsNoTracking().Any(product => product.Id == createdProduct.Id);

            // Assert
            Assert.True(productExists);
            createdProduct.Should().BeEquivalentTo(createProductPayload, options => options.Excluding(p => p.Id));
            Assert.NotEqual(0, createdProduct.Id);
        }
        
        [Fact]
        public async Task UpdateProductTest()
        {
            // Arrange
            var productsService = new ProductsService(_fixture.dbContext);

            // Get a sample product from the in-memory database
            var existingProduct = _fixture.dbContext.Products.AsNoTracking().ToList().First();
            var updateProductPayload = new Product
            {
                Id = existingProduct.Id,
                Name = "Updated Name",
                AgeRestriction = existingProduct.AgeRestriction,
                Description = "Updated Description",
                Company = "Updated Company Name",
                Price = existingProduct.Price,
            };

            // Act
            var operationResult = await productsService.UpdateProduct(updateProductPayload.Id, updateProductPayload);
            var updatedProduct = _fixture.dbContext.Products.AsNoTracking().ToList().First(product => product.Id == updateProductPayload.Id);

            // Assert
            Assert.True(operationResult);
            updatedProduct.Should().BeEquivalentTo(updateProductPayload, options => options
            .Excluding(p => p.Name).Excluding(p => p.Description).Excluding(p => p.Company));
            Assert.Equal(updatedProduct.Name, updateProductPayload.Name);
            Assert.Equal(updatedProduct.Description, updateProductPayload.Description);
            Assert.Equal(updatedProduct.Company, updateProductPayload.Company);
        }

        [Fact]
        public async Task DeleteProductByIdTest()
        {
            // Arrange
            var productsService = new ProductsService(_fixture.dbContext);

            // Get a sample product from the in-memory database
            var existingProduct = _fixture.dbContext.Products.AsNoTracking().First();
            var productUpdatePayload = new Product
            {
                Id = existingProduct.Id,
                Name = "Updated Name",
                AgeRestriction = existingProduct.AgeRestriction,
                Description = "Updated Description",
                Company = "Updated Company Name",
                Price = existingProduct.Price,
            };

            // Act
            var operationResult = await productsService.DeleteProductById(existingProduct.Id);
            var deletedProduct = _fixture.dbContext.Products.FirstOrDefault(p => p.Id == existingProduct.Id);

            // Assert
            Assert.True(operationResult);
            Assert.Null(deletedProduct);
        }
    }
}