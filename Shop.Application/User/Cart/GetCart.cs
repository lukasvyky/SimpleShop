using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Models;
using System.Text;
using System.Text.Json;

namespace Shop.Application.User.Cart
{
    public class GetCart
    {
        private ISession Session { get; }
        private ApplicationDbContext Context { get; }

        public GetCart(ISessionService sessionService, ApplicationDbContext ctx)
        {
            Session = sessionService.GetSession();
            Context = ctx;
        }

        public IEnumerable<Response> Do()
        {
            var hasCookieValue = Session.TryGetValue("cart", out byte[] value);

            if (!hasCookieValue)
            {
                return new List<Response>();
            }

            var cartItems = JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(value));
            var response = Context.Stocks
                .Include(s => s.Product)
                .AsEnumerable()
                .Where(s => cartItems.Any(cp => cp.StockId == s.Id))
                .Select(s => new Response()
                {
                    Name = s.Product.Name,
                    Value = $"CZK {s.Product.Value:N2}",
                    StockId = s.Id,
                    Qty = cartItems.FirstOrDefault(cp => cp.StockId == s.Id).Qty
                })
                .ToList();

            return response;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
