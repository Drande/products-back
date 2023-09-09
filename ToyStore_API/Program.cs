using Microsoft.EntityFrameworkCore;
using ToyStore_API.Data;
using ToyStore_API.Interfaces.Services;
using ToyStore_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // Add other custom options here if needed
});
const string CorsPolicyName = "CORS_POLICY";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicyName,
        policy =>
        {
            policy.WithOrigins("*");
            policy.WithHeaders("*");
            policy.WithMethods("*");
        });
});
builder.Services.AddScoped<IProductsService, ProductsService>();

var app = builder.Build();

app.UseCors(CorsPolicyName);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }