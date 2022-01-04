using Shop.Database;
using Shop.Domain.Enums;

namespace Shop.Application.Admin.OrdersAdmin
{
    public class GetOrdersAdmin
    {
        private ApplicationDbContext Context { get; }

        public GetOrdersAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<Response> Do(int status)
        {
            return Context.Orders.Where(o => o.OrderStatus == (OrderStatus)status)
                .Select(o => new Response()
                {
                    Id = o.Id,
                    OrderRef = o.OrderRef,
                    Email = o.Email
                });
        }
        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string Email { get; set; }
        }
    }
}
