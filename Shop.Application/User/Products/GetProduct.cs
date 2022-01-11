using Shop.Domain.Infrastructure;

namespace Shop.Application.User.Products
{
    [Service]
    public class GetProduct
    {
        private IStockService StockService { get; }
        private IProductService ProductService { get; }

        public GetProduct(IStockService stockService, IProductService productService)
        {
            StockService = stockService;
            ProductService = productService;
        }

        public async Task<ProductViewModel> Do(string productName)
        {
            await StockService.RetrieveExpiredStockOnHold();

            return ProductService.GetProductByName(productName, p => new ProductViewModel
            {
                Name = p.Name,
                Description = p.Description,
                Value = p.Value.ConvertPriceToText(),
                Stock = p.Stock.Select(s => new StockViewModel()
                {
                    Id = s.Id,
                    Description = s.Description,
                    Qty = s.Qty
                })
            });
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }

            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
