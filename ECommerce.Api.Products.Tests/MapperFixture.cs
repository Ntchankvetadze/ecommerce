using AutoMapper;
using ECommerce.Api.Products.Profiles;

namespace ECommerce.Api.Products.Tests
{
    public class MapperFixture : IDisposable
    {
        public Mapper mapper { get; private set; }

        public MapperFixture()
        {
            SetupMapper();
        }

        public void Dispose()
        {
        }

        private void SetupMapper()
        {
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(c => c.AddProfile(productProfile));
            mapper = new Mapper(configuration);
        }
    }
}
