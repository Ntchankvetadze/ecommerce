using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedDate();
        }

        private void SeedDate()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Product { Id = 1, Name = "Keyboard", Price = 43, Inventory = 50} );
                dbContext.Products.Add(new Product { Id = 2, Name = "Mouse", Price = 15, Inventory = 153 });
                dbContext.Products.Add(new Product { Id = 3, Name = "Monitor", Price = 276, Inventory = 30 });
                dbContext.Products.Add(new Product { Id = 4, Name = "CPU", Price = 350, Inventory = 567 });
                dbContext.Products.Add(new Product { Id = 5, Name = "Memory", Price = 150, Inventory = 345 });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetAllProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                if(products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Models.Product>>(products);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(m => m.Id == id);

                if (product != null)
                {
                    var result = mapper.Map<Models.Product>(product);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}
