using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.ProductsAdmin
{
    public class GetProductAdmin
    {
        private IProductService ProductService { get; }

        public GetProductAdmin(IProductService productService)
        {
            ProductService = productService;
        }

        public ProductViewModel Do(int id) =>
            ProductService.GetProductById(id, p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Value = p.Value
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
