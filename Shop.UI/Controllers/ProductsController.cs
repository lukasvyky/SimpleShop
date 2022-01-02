using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Database;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class ProductsController : ControllerBase
    {
        private ApplicationDbContext Context { get; }
        public ProductsController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetProducts() => Ok(new GetProducts(Context).Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(Context).Do(id));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request) => Ok(await new CreateProduct(Context).Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(Context).Do(id));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request) => Ok(await new UpdateProduct(Context).Do(request));
    }
}