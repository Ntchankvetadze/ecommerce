using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer { Id = 1, Name = "George", Address = "Tbilisi" });
                dbContext.Customers.Add(new Customer { Id = 2, Name = "David", Address = "Moscow" });
                dbContext.Customers.Add(new Customer { Id = 3, Name = "Constantin", Address = "Athen" });
                dbContext.Customers.Add(new Customer { Id = 4, Name = "John", Address = "New York" });
                dbContext.Customers.Add(new Customer { Id = 5, Name = "Anna", Address = "Munich" });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetAllCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                if(customers is not null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                else
                {
                    return (false, null, null);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(m => m.Id == id);

                if (customer is not null)
                {
                    var result = mapper.Map<Models.Customer>(customer);
                    return (true, result, null);
                }
                else
                {
                    return (false, null, null);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
