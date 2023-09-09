using ToyStore_API.Models.Products;

namespace ToyStore_API.Interfaces.Services
{
    public interface IProductsService
    {
        public Task<IEnumerable<Product>> GetProductList();
        public Task<Product?> GetProductById(int id);
        public Task<Product> CreateProduct(Product product);
        public Task<bool> UpdateProduct(int id, Product product);
        public Task<bool> DeleteProductById(int id);
        public bool ProductExists(int id);
    }
}
