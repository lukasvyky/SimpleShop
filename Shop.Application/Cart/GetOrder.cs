using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Models;
using System.Text;
using System.Text.Json;

namespace Shop.Application.Cart
{
    public class GetOrder
    {
        private ISession Session { get; }
        private ApplicationDbContext Context { get; }

        public GetOrder(ISession session, ApplicationDbContext ctx)
        {
            Session = session;
            Context = ctx;
        }

        public Response Do()
        {
            var hasCartValue = Session.TryGetValue("cart", out byte[] cartValue);

            if (!hasCartValue)
            {
                return new Response();
            }

            var cartItems = JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(cartValue));

            var products = Context.Stocks
                .Include(s => s.Product)
                .AsEnumerable()
                .Where(s => cartItems.Any(cp => cp.StockId == s.Id))
                .Select(s => new Product()
                {
                    ProductId = s.Product.Id,
                    Value = (int)(s.Product.Value * 100),
                    StockId = s.Id,
                    Qty = cartItems.FirstOrDefault(cp => cp.StockId == s.Id).Qty
                })
                .ToList();

            var hasCustomerValue = Session.TryGetValue("customer-information", out byte[] customerValue);
            var customerIntel = hasCustomerValue ? JsonSerializer.Deserialize<CustomerInformation>(Encoding.ASCII.GetString(customerValue)) : null;

            return new Response()
            {
                Products = products,
                CustomerInformation = customerIntel
            };
        }

        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int GetTotalCharge() => Products.Sum(p => p.Value * p.Qty);
        }

        public class Product
        {
            public int ProductId { get; set; }
            public int Value { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
