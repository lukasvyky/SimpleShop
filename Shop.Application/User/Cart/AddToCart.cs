using Microsoft.AspNetCore.Http;
using Shop.Database;
using Shop.Domain.Models;
using System.Text;
using System.Text.Json;

namespace Shop.Application.User.Cart
{
    public class AddToCart
    {
        private ISession Session { get; }
        private ApplicationDbContext Context { get; }

        public AddToCart(ISessionService sessionService, ApplicationDbContext context)
        {
            Session = sessionService.GetSession();
            Context = context;
        }

        public async Task<bool> Do(Request request)
        {
            var stocksOnHold = Context.StockOnHold.Where(s => s.SessionId == Session.Id).ToList();
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
                    SessionId = Session.Id,
                    Qty = request.Qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });
            }

            stockToHold.Qty -= request.Qty;

            Context.StockOnHold.Where(s => s.SessionId == Session.Id).ToList().ForEach(s => s.ExpiryDate = DateTime.Now.AddMinutes(20));
            await Context.SaveChangesAsync();


            var hasCookieValue = Session.TryGetValue("cart", out byte[] value);
            var cartItems = new List<CartProduct>();

            if (hasCookieValue)
            {
                cartItems = JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(value));
            }

            if (cartItems.Any(cp => cp.StockId == request.StockId))
            {
                cartItems.Find(cp => cp.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                cartItems.Add(new CartProduct()
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }

            var stringObject = JsonSerializer.Serialize(cartItems);

            Session.Set("cart", Encoding.UTF8.GetBytes(stringObject));

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
