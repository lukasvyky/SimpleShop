using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.ProductsAdmin
{
    [Service]
    public class GetProductsAdmin
    {
        private IProductService ProductService { get; }

        public GetProductsAdmin(IProductService productService)
        {
            ProductService = productService;
        }

        public IEnumerable<ProductViewModel> Do() =>
            ProductService.GetProductsWithStock(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Value = p.Value
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
    }
}
