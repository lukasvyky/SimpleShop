using Microsoft.AspNetCore.Mvc;
using Shop.Application.User.Cart;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
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

        [HttpPost("{stockId:int}/{qty:int}")]
        public async Task<IActionResult> Remove([FromServices] RemoveFromCart removeFromCart, [FromRoute] int stockId, [FromRoute] int qty)
        {
            var success = await removeFromCart.Do(new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = qty
            });

            if (!success)
            {
                return BadRequest("Failed to remove items from cart");
            }

            return Ok("Items removed from cart");
        }

        [HttpGet]
        public IActionResult GetCartSmallComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Do().Sum(x => x.RealValue * x.Qty);

            return PartialView("Components/Cart/Small", $"£{totalValue}");
        }

        [HttpGet]
        public IActionResult GetCartMainComponent([FromServices] GetCart getCart)
        {
            var cart = getCart.Do();

            return PartialView("_CartPartial", cart);
        }
    }
}
