using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToyStore_API.Migrations
{
    /// <inheritdoc />
    public partial class SetupDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AgeRestriction = table.Column<int>(type: "int", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRestriction", "Company", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, "Dell", "Powerful laptop with high-resolution display.", "Laptop", 999.99m },
                    { 2, 16, "Apple", "Latest smartphone with advanced camera features.", "Smartphone", 799.99m },
                    { 3, null, "Samsung", "Lightweight tablet with long battery life.", "Tablet", 349.99m },
                    { 4, 12, "Sony", "Wireless headphones with noise-cancellation technology.", "Headphones", 149.99m },
                    { 5, null, "Fitbit", "Fitness tracking smartwatch with heart rate monitor.", "Smartwatch", 129.99m },
                    { 6, 18, "Canon", "Digital camera with high-quality lens.", "Camera", 599.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
