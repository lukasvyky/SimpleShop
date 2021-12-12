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
            var cartProduct = new CartProduct()
            {
                StockId = request.StockId,
                Qty = request.Qty
            };

            var stringObject = JsonSerializer.Serialize(cartProduct);

            Session.Set("cart", Encoding.UTF8.GetBytes(stringObject));
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
