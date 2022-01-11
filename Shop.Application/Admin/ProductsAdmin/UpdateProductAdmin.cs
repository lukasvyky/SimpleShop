using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.ProductsAdmin
{
    [Service]
    public class UpdateProductAdmin
    {
        private IProductService ProductService { get; }

        public UpdateProductAdmin(IProductService productService)
        {
            ProductService = productService;
        }

        public async Task<Response> Do(Request request)
        {
            var product = ProductService.GetProductById(request.Id, s => s);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;

            await ProductService.UpdateProduct(product);

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public class Request
        {
            public int Id { get; set; }
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
