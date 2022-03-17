namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetAllCustomersAsync();

        Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
