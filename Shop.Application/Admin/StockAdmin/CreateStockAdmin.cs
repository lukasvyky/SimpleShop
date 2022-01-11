using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Application.Admin.StockAdmin
{
    public class CreateStockAdmin
    {
        private IStockService StockService { get; }

        public CreateStockAdmin(IStockService stockService)
        {
            StockService = stockService;
        }

        public async Task<Response> Do(Request request)
        {
            var stockToSave = new Stock
            {
                ProductId = request.ProductId,
                Description = request.Description,
                Qty = request.Qty
            };

            await StockService.CreateStock(stockToSave);

            return new Response
            {
                Id = stockToSave.Id,
                Description = stockToSave.Description,
                Qty = stockToSave.Qty
            };
        }

        public class Request
        {
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

    }
}
