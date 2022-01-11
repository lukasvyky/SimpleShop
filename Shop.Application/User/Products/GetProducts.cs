using Shop.Domain.Infrastructure;

namespace Shop.Application.User.Products
{
    public class GetProducts
    {
        private IProductService ProductService { get; }

        public GetProducts(IProductService productService)
        {
            ProductService = productService;
        }

        public IEnumerable<ProductViewModel> Do() =>
            ProductService.GetProductsWithStock(p => new ProductViewModel
            {
                Name = p.Name,
                Description = p.Description,
                Value = p.Value.ConvertPriceToText(),
                StockCount = p.Stock.Sum(s => s.Qty)
            });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }
        }
    }
}
