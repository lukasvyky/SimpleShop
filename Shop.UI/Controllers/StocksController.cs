using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Admin.StockAdmin;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class StocksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStocks([FromServices] GetStockAdmin getStock)
            => Ok(getStock.Do());

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromServices] CreateStockAdmin createStock, [FromBody] CreateStockAdmin.Request request)
            => Ok(await createStock.Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromServices] DeleteStockAdmin deleteStock, int id)
            => Ok(await deleteStock.Do(id));

        [HttpPut]
        public async Task<IActionResult> UpdateStock([FromServices] UpdateStockAdmin updateStock, [FromBody] UpdateStockAdmin.Request request)
            => Ok(await updateStock.Do(request));
    }
}