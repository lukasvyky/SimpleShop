using Shop.Application.Infrastructure;
using Shop.Database;

namespace Shop.Application.User.Cart
{
    public class RemoveFromCart
    {
        private ISessionService SessionService { get; }
        private ApplicationDbContext Context { get; }

        public RemoveFromCart(ISessionService sessionService, ApplicationDbContext context)
        {
            SessionService = sessionService;
            Context = context;
        }

        public async Task<bool> Do(Request request)
        {
            var originalStock = Context.Stocks.Where(s => s.Id == request.StockId).FirstOrDefault();
            var stockOnHold = Context.StockOnHold.FirstOrDefault(s => s.StockId == request.StockId && s.SessionId == SessionService.GetId());

            if (request.RemoveAll || stockOnHold.Qty <= request.Qty)
            {
                originalStock.Qty += stockOnHold.Qty;
                Context.StockOnHold.Remove(stockOnHold);
                SessionService.RemoveProduct(request.StockId, request.Qty);
            }
            else
            {
                originalStock.Qty += request.Qty;
                stockOnHold.Qty -= request.Qty;
                SessionService.RemoveProduct(request.StockId, request.Qty);
            }

            await Context.SaveChangesAsync();

            SessionService.RemoveProduct(request.StockId, request.Qty);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
            public bool RemoveAll { get; set; }
        }
    }
}
