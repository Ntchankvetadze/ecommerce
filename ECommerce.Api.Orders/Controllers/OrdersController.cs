using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetCustomerOrdersAsync(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }
    }
}
