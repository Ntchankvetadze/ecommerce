using Microsoft.AspNetCore.Mvc;
using ECommerce.Api.Customers.Interfaces;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var result = await customersProvider.GetAllCustomersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }
    }
}
