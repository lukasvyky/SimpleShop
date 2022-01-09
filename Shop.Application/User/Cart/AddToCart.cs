using Shop.Application.Infrastructure;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.User.Cart
{
    public class AddToCart
    {
        private ISessionService SessionService { get; }
        private ApplicationDbContext Context { get; }

        public AddToCart(ISessionService sessionService, ApplicationDbContext context)
        {
            SessionService = sessionService;
            Context = context;
        }

        public async Task<bool> Do(Request request)
        {
            var stocksOnHold = Context.StockOnHold.Where(s => s.SessionId == SessionService.GetId()).ToList();
            var stockToHold = Context.Stocks.Where(s => s.Id == request.StockId).FirstOrDefault();

            if (stockToHold.Qty < request.Qty)
            {
                return false;
            }

            if (stocksOnHold.Any(s => s.StockId == request.StockId))
            {
                stocksOnHold.FirstOrDefault(s => s.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                Context.StockOnHold.Add(new StockOnHold()
                {
                    StockId = stockToHold.Id,
                    SessionId = SessionService.GetId(),
                    Qty = request.Qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });
            }

            stockToHold.Qty -= request.Qty;

            Context.StockOnHold.Where(s => s.SessionId == SessionService.GetId()).ToList().ForEach(s => s.ExpiryDate = DateTime.Now.AddMinutes(20));
            await Context.SaveChangesAsync();

            SessionService.AddProduct(request.StockId, request.Qty);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
