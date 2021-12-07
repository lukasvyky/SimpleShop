using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Database;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private ApplicationDbContext Context { get; }
        public AdminController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(Context).Do());

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(Context).Do(id));

        [HttpPost("products")]
        public IActionResult CreateProduct(CreateProduct.ProductViewModel vm) => Ok(new CreateProduct(Context).Do(vm));

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id) => Ok(new DeleteProduct(Context).Do(id));

        [HttpPut("products")]
        public IActionResult UpdateProduct(UpdateProduct.ProductViewModel vm) => Ok(new UpdateProduct(Context).Do(vm));

    }
}
