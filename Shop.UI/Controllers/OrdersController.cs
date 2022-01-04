using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrdersAdmin;
using Shop.Database;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class OrdersController : ControllerBase
    {
        private ApplicationDbContext Context { get; }
        public OrdersController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            return Ok(new GetOrder(Context).Do(id));
        }

        [HttpGet]
        public IActionResult GetOrders(int status)
        {
            return Ok(new GetOrders(Context).Do(status));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            return Ok(await new UpdateOrder(Context).Do(id));
        }
    }
}
