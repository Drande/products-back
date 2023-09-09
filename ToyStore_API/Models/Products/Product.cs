using System.ComponentModel.DataAnnotations;

namespace ToyStore_API.Models.Products
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }

        [Range(0, 100)]
        public int? AgeRestriction { get; set; }

        [Required]
        [MaxLength(50)]
        public string Company { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; } = 0m;

        public Product UpdateWith(Product updates)
        {
            this.Name = updates.Name;
            this.Description = updates.Description;
            this.AgeRestriction = updates.AgeRestriction;
            this.Company = updates.Company;
            this.Price = updates.Price;
            return this;
        }
    }
}
