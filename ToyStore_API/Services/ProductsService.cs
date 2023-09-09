using Microsoft.EntityFrameworkCore;
using ToyStore_API.Data;
using ToyStore_API.Interfaces.Services;
using ToyStore_API.Models.Products;

namespace ToyStore_API.Services
{
    public class ProductsService: IProductsService
    {
        private readonly AppDbContext _dbContext;
        public ProductsService(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductList()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> CreateProduct(Product product) {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProduct(int id, Product productUpdate)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync(id);
                if (product == null) return false;

                product.UpdateWith(productUpdate);
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteProductById(int id)
        {

            if (_dbContext.Products == null)
            {
                return false;
            }
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public bool ProductExists(int id)
        {
            return (_dbContext.Products.Any(e => e.Id == id));
        }
    }
}
