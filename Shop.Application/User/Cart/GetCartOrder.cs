using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.User.Cart
{
    public class GetCartOrder
    {
        private ISessionService SessionService { get; }

        public GetCartOrder(ISessionService sessionService)
        {
            SessionService = sessionService;
        }

        public Response Do()
        {
            var cartItems = SessionService.GetCart(p => new Product
            {
                ProductId = p.ProductId,
                Value = (int)(p.Value * 100),
                StockId = p.StockId,
                Qty = p.Qty
            });
            var customerInformation = SessionService.GetCustomerInformation();

            return customerInformation is null ? null : new Response
            {
                Products = cartItems,
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
