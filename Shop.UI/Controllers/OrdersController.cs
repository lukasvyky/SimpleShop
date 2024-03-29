﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Admin.OrdersAdmin;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetOrder([FromServices] GetOrderAdmin getOrderAdmin, int id)
            => Ok(getOrderAdmin.Do(id));

        [HttpGet]
        public IActionResult GetOrders([FromServices] GetOrdersAdmin getOrdersAdmin, int status)
            => Ok(getOrdersAdmin.Do(status));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromServices] UpdateOrderAdmin updateOrderAdmin, int id)
        {
            var success = await updateOrderAdmin.Do(id) > 0;
            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
