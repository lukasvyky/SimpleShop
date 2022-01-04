using Microsoft.EntityFrameworkCore;
using Shop.Database;

namespace Shop.Application.User.Products
{
    public class GetProduct
    {
        private ApplicationDbContext Context { get; }
        public GetProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<ProductViewModel> Do(string productName)
        {

            var stocksOnHold = Context.StockOnHold.Where(s => s.ExpiryDate < DateTime.Now).ToList();

            if (stocksOnHold.Any())
            {
                var stockToReturn = Context.Stocks.AsEnumerable().Where(s => stocksOnHold.Any(sh => sh.StockId == s.Id));

                foreach (var stock in stockToReturn)
                {
                    stock.Qty += stocksOnHold.FirstOrDefault(s => s.StockId == stock.Id).Qty;
                }

                Context.StockOnHold.RemoveRange(stocksOnHold);
                await Context.SaveChangesAsync();
            }

            return Context.Products
                    .Include(p => p.Stock)
                    .Where(p => p.Name.Equals(productName))
                    .Select(p => new ProductViewModel

                    {
                        Name = p.Name,
                        Description = p.Description,
                        Value = $"CZK{p.Value.ToString("N2")}",
                        Stock = p.Stock.Select(s => new StockViewModel()
                        {
                            Id = s.Id,
                            Description = s.Description,
                            InStock = s.Qty > 0
                        })
                    })
                    .FirstOrDefault();
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
            public bool InStock { get; set; }
        }
    }
}
