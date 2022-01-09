using Shop.Domain.Infrastructure;

namespace Shop.Application.User.Cart
{
    public class RemoveFromCart
    {
        private ISessionService SessionService { get; }
        private IStockService StockService { get; }

        public RemoveFromCart(ISessionService sessionService, IStockService stockService)
        {
            SessionService = sessionService;
            StockService = stockService;
        }
       
        public async Task<bool> Do(Request request)
        {
            if (request.Qty <= 0)
            {
                return false;
            }
            await StockService.RemoveStockFromHold(request.StockId, request.StockId, SessionService.GetId());

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
