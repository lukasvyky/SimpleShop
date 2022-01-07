using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.User.Cart
{
    public class RemoveFromCart
    {
        private ISession Session { get; }
        private ApplicationDbContext Context { get; }

        public RemoveFromCart(ISessionService sessionService, ApplicationDbContext context)
        {
            Session = sessionService.GetSession();
            Context = context;
        }

        public async Task<bool> Do(Request request)
        {
            var originalStock = Context.Stocks.Where(s => s.Id == request.StockId).FirstOrDefault();
            var stockOnHold = Context.StockOnHold.FirstOrDefault(s => s.StockId == request.StockId && s.SessionId == Session.Id);

            if (request.RemoveAll || stockOnHold.Qty <= request.Qty)
            {
                originalStock.Qty += stockOnHold.Qty;
                Context.StockOnHold.Remove(stockOnHold);
            }
            else
            {
                originalStock.Qty += request.Qty;
                stockOnHold.Qty -= request.Qty;
            }

            await Context.SaveChangesAsync();

            var hasCookieValue = Session.TryGetValue("cart", out byte[] value);
            var cartItems = new List<CartProduct>();

            if (hasCookieValue)
            {
                cartItems = JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(value));
            }

            if (cartItems.Any(cp => cp.StockId == request.StockId))
            {
                cartItems.Find(cp => cp.StockId == request.StockId).Qty -= request.Qty;
            }

            var stringObject = JsonSerializer.Serialize(cartItems);

            Session.Set("cart", Encoding.UTF8.GetBytes(stringObject));

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
