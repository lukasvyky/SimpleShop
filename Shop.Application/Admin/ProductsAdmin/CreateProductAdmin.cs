using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Application.Admin.ProductsAdmin
{
    [Service]
    public class CreateProductAdmin
    {
        private IProductService ProductService { get; }

        public CreateProductAdmin(IProductService productService)
        {
            ProductService = productService;
        }

        public async Task<Response> Do(Request request)
        {
            var newProduct = new Product
            {
                Value = request.Value,
                Name = request.Name,
                Description = request.Description
            };

            if (await ProductService.CreateProduct(newProduct) <= 0)
            {
                throw new Exception("Failed to create product");
            }

            return new Response
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Value = newProduct.Value
            };
        }
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
