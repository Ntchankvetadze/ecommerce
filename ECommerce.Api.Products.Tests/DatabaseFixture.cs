using ECommerce.Api.Products.Db;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public ProductsDbContext dbContext { get; private set; }

        public DatabaseFixture()
        {
            SetupDbContext();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        private void SetupDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase("TestProducts").Options;

            dbContext = new ProductsDbContext(dbContextOptions);
        }
    }
}
