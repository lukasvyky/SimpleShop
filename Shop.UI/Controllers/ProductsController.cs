using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Admin.ProductsAdmin;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "Manager")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts([FromServices] GetProductsAdmin getProducts)
            => Ok(getProducts.Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromServices] GetProductAdmin getProduct, int id)
            => Ok(getProduct.Do(id));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromServices] CreateProductAdmin createProduct, [FromBody] CreateProductAdmin.Request request)
            => Ok(await createProduct.Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromServices] DeleteProductAdmin deleteProduct, int id)
            => Ok(await deleteProduct.Do(id));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromServices] UpdateProductsAdmin updateProducts, [FromBody] UpdateProductsAdmin.Request request)
            => Ok(await updateProducts.Do(request));
    }
}