using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.StockAdmin;
using Shop.Database;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class StocksController : ControllerBase
    {
        private ApplicationDbContext Context { get; }
        public StocksController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet("stocks")]
        public IActionResult GetStocks() => Ok(new GetStock(Context).Do());

        [HttpPost("stocks")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request) => Ok(await new CreateStock(Context).Do(request));

        [HttpDelete("stocks/{id}")]
        public async Task<IActionResult> DeleteStock(int id) => Ok(await new DeleteStock(Context).Do(id));

        [HttpPut("stocks")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request) => Ok(await new UpdateStock(Context).Do(request));
    }
}
