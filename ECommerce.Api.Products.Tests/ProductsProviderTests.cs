using Xunit;
using ECommerce.Api.Products.Providers;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsProviderTests : IClassFixture<DatabaseFixture>, IClassFixture<MapperFixture>
    {
        DatabaseFixture databaseFixture;
        MapperFixture mapperFixture;

        public ProductsProviderTests(DatabaseFixture databaseFixture, MapperFixture mapperFixture)
        {
            this.databaseFixture = databaseFixture;
            this.mapperFixture = mapperFixture;
        }

        [Fact]
        public async Task GetAllProductsAsync_WhenIsOk_ReturnsAllProducts()
        {
            var sut = new ProductsProvider(databaseFixture.dbContext, null, mapperFixture.mapper);

            var productsResult = await sut.GetAllProductsAsync();

            Assert.True(productsResult.IsSuccess);
            Assert.Null(productsResult.ErrorMessage);
            Assert.NotEmpty(productsResult.Products);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetProductsAsync_WhenIdExists_ReturnsProduct(int id)
        {
            var sut = new ProductsProvider(databaseFixture.dbContext, null, mapperFixture.mapper);

            var productsResult = await sut.GetProductAsync(id);

            Assert.True(productsResult.IsSuccess);
            Assert.Null(productsResult.ErrorMessage);
            Assert.NotNull(productsResult.Product);
            Assert.Equal(id, productsResult.Product.Id);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(12)]
        public async Task GetProductsAsync_WhenInvalidId_ReturnsUnsuccessfulResult(int id)
        {
            var sut = new ProductsProvider(databaseFixture.dbContext, null, mapperFixture.mapper);

            var productsResult = await sut.GetProductAsync(id);

            Assert.False(productsResult.IsSuccess);
            Assert.NotNull(productsResult.ErrorMessage);
            Assert.Null(productsResult.Product);
        }
    }
}