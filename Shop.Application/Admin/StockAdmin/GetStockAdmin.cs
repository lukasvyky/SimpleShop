using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.StockAdmin
{
    public class GetStockAdmin
    {
        private IProductService ProductService { get; }
        public GetStockAdmin(IProductService productService)
        {
            ProductService = productService;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return ProductService.GetProductsWithStock(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Stock = p.Stock.Select(x => new StockViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Qty = x.Qty
                })
            });
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }


}
