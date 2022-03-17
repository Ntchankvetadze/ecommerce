namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync();

        Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
