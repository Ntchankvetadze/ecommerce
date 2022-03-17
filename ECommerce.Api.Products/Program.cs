using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Custom config
// Add database

builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseInMemoryDatabase("Products");
});

// Add mapper
builder.Services.AddAutoMapper(typeof(Program));

// Add services
builder.Services.AddScoped<IProductsProvider, ProductsProvider>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
