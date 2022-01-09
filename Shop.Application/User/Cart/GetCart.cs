using Microsoft.EntityFrameworkCore;
using Shop.Application.Infrastructure;
using Shop.Database;

namespace Shop.Application.User.Cart
{
    public class GetCart
    {
        private ISessionService SessionService { get; }
        private ApplicationDbContext Context { get; }

        public GetCart(ISessionService sessionService, ApplicationDbContext ctx)
        {
            SessionService = sessionService;
            Context = ctx;
        }

        public IEnumerable<Response> Do()
        {
            var cartItems = SessionService.GetCart();

            return cartItems is null ? new List<Response>() : Context.Stocks
                .Include(s => s.Product)
                .AsEnumerable()
                .Where(s => cartItems.Any(cp => cp.StockId == s.Id))
                .Select(s => new Response()
                {
                    Name = s.Product.Name,
                    Value = $"CZK {s.Product.Value:N2}",
                    RealValue = s.Product.Value,
                    StockId = s.Id,
                    Qty = cartItems.FirstOrDefault(cp => cp.StockId == s.Id).Qty
                })
                .ToList();
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
