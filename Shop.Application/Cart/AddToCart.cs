using Microsoft.AspNetCore.Http;
using Shop.Domain.Models;
using System.Text;
using System.Text.Json;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession Session { get; }
        public AddToCart(ISession session)
        {
            Session = session;
        }

        public void Do(Request request)
        {
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
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
