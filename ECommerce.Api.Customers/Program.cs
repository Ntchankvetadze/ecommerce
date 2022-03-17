using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Custom services
builder.Services.AddDbContext<CustomersDbContext>(options =>
{
    options.UseInMemoryDatabase("Customers");
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();
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
