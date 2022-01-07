using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.User.Cart;

namespace Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : ControllerBase
    {
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> AddOne([FromServices] AddToCart addToCart, [FromRoute] int stockId)
        {
            var success = await addToCart.Do(new AddToCart.Request()
            {
                StockId = stockId,
                Qty = 1
            });

            if (!success)
            {
                return BadRequest("Failed to add to cart");
            }

            return Ok("Item added to cart");
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> RemoveOne([FromServices] RemoveFromCart removeFromCart, [FromRoute] int stockId)
        {
            var success = await removeFromCart.Do(new RemoveFromCart.Request()
            {
                StockId = stockId,
                Qty = 1
            });

            if (!success)
            {
                return BadRequest("Failed to remove item from cart");
            }

            return Ok("Item removed from cart");
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> RemoveAll([FromServices] RemoveFromCart removeFromCart, [FromRoute] int stockId)
        {
            var success = await removeFromCart.Do(new RemoveFromCart.Request()
            {
                StockId = stockId,
                RemoveAll = true
            });

            if (!success)
            {
                return BadRequest("Failed to remove items from cart");
            }

            return Ok("Items removed from cart");
        }
    }
}
