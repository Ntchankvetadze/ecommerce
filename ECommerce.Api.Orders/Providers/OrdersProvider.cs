using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Add(new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-4),
                    Total = 150,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 5, UnitPrice = 30},
                    }
                });

                dbContext.Add(new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now.AddDays(-40),
                    Total = 345.67m,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { Id = 2, OrderId = 2, ProductId = 1, Quantity = 5, UnitPrice = 30},
                        new OrderItem { Id = 3, OrderId = 2, ProductId = 2, Quantity = 5, UnitPrice = 30},
                        new OrderItem { Id = 4, OrderId = 2, ProductId = 3, Quantity = 5, UnitPrice = 30},
                    }
                });

                dbContext.Add(new Order
                {
                    Id = 3,
                    CustomerId = 3,
                    OrderDate = DateTime.Now.AddDays(-15),
                    Total = 650.67m,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { Id = 5, OrderId = 3, ProductId = 1, Quantity = 5, UnitPrice = 30},
                        new OrderItem { Id = 6, OrderId = 3, ProductId = 1, Quantity = 5, UnitPrice = 30},
                    }
                });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetCustomerOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Include(o => o.Items)
                    .Where(o => o.CustomerId == customerId).ToListAsync();

                if (orders is not null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Models.Order>>(orders);

                    return (true, result, null);
                } 

                return (false, null, null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}
