using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.StockAdmin
{
    public class CreateStock
    {
        private ApplicationDbContext Context { get; }
        public CreateStock(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var stockToSave = new Stock
            {
                ProductId = request.ProductId,
                Description = request.Description,
                Qty = request.Qty
            };

            Context.Stocks.Add(stockToSave);
            await Context.SaveChangesAsync();

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
