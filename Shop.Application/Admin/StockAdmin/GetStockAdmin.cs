using Microsoft.EntityFrameworkCore;
using Shop.Database;

namespace Shop.Application.Admin.StockAdmin
{
    public class GetStockAdmin
    {
        private ApplicationDbContext Context { get; }
        public GetStockAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            var stock = Context.Products
                .Include(p => p.Stock)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Stock = p.Stock.Select(x => new StockViewModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Qty = x.Qty
                    })
                })
                .ToList();

            return stock;
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
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }


}
