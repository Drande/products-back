using ToyStore_API.Models.Products;

namespace ToyStore_API.Data.Seed
{
    public static class ProductsSeeds
    {
        public static readonly List<Product> Sample1 = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "Powerful laptop with high-resolution display.",
                AgeRestriction = null,
                Company = "Dell",
                Price = 999.99m
            },
            new Product
            {
                Id = 2,
                Name = "Smartphone",
                Description = "Latest smartphone with advanced camera features.",
                AgeRestriction = 16,
                Company = "Apple",
                Price = 799.99m
            },
            new Product
            {
                Id = 3,
                Name = "Tablet",
                Description = "Lightweight tablet with long battery life.",
                AgeRestriction = null,
                Company = "Samsung",
                Price = 349.99m
            },
            new Product
            {
                Id = 4,
                Name = "Headphones",
                Description = "Wireless headphones with noise-cancellation technology.",
                AgeRestriction = 12,
                Company = "Sony",
                Price = 149.99m
            },
            new Product
            {
                Id = 5,
                Name = "Smartwatch",
                Description = "Fitness tracking smartwatch with heart rate monitor.",
                AgeRestriction = null,
                Company = "Fitbit",
                Price = 129.99m
            },
            new Product
            {
                Id = 6,
                Name = "Camera",
                Description = "Digital camera with high-quality lens.",
                AgeRestriction = 18,
                Company = "Canon",
                Price = 599.99m
            },
        };
    }
}
