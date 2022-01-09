using Microsoft.EntityFrameworkCore;
using Shop.Application.Infrastructure;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.User.Cart
{
    public class GetCartOrder
    {
        private ISessionService SessionService { get; }
        private ApplicationDbContext Context { get; }

        public GetCartOrder(ISessionService sessionService, ApplicationDbContext ctx)
        {
            SessionService = sessionService;
            Context = ctx;
        }

        public Response Do()
        {
            var cartItems = SessionService.GetCart();

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

            var customerInformation = SessionService.GetCustomerInformation();

            return customerInformation is null ? null : new Response
            {
                Products = products,
                CustomerInformation = customerInformation
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
