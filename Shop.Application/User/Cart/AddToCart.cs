using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Application.User.Cart
{
    public class AddToCart
    {
        private ISessionService SessionService { get; }
        private IStockService StockService { get; }

        public AddToCart(ISessionService sessionService, IStockService stockService)
        {
            SessionService = sessionService;
            StockService = stockService;
        }

        public async Task<bool> Do(Request request)
        {
            if (!StockService.IsThereEnoughStock(request.StockId, request.Qty))
            {
                return false;
            }

            await StockService.PutStockOnHold(request.StockId, request.Qty, SessionService.GetId());

            var stock = StockService.GetStockWithProduct(request.StockId);

            var cartProduct = new CartProduct()
            {
                ProductId = stock.Product.Id,
                ProductName = stock.Product.Name,
                StockId = stock.Id,
                Qty = request.Qty,
                Value = stock.Product.Value

            };

            SessionService.AddProduct(cartProduct);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
