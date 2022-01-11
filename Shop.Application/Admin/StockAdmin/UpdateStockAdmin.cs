using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Application.Admin.StockAdmin
{
    [Service]
    public class UpdateStockAdmin
    {
        private IStockService StockService { get; }

        public UpdateStockAdmin(IStockService stockService)
        {
            StockService = stockService;
        }

        public async Task<Response> Do(Request request)
        {
            var stocks = new List<Stock>();

            foreach (var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Qty = stock.Qty,
                    ProductId = stock.ProductId
                });
            }

            await StockService.UpdateStockRange(stocks);

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
