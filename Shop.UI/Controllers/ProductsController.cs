﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Admin.ProductsAdmin;
using System.Threading.Tasks;

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
        public async Task<IActionResult> UpdateProduct([FromServices] UpdateProductAdmin updateProduct, [FromBody] UpdateProductAdmin.Request request)
            => Ok(await updateProduct.Do(request));
    }
}