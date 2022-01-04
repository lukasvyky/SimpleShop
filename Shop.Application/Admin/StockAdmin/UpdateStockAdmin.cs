using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Admin.StockAdmin
{
    public class UpdateStockAdmin
    {
        private ApplicationDbContext Context { get; }
        public UpdateStockAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var stockList = new List<Stock>();

            foreach (var stock in request.Stock)
            {
                stockList.Add(new Stock
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Qty = stock.Qty,
                    ProductId = stock.ProductId
                });
            }

            Context.UpdateRange(stockList);
            await Context.SaveChangesAsync();

            return new Response
            {
                Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
