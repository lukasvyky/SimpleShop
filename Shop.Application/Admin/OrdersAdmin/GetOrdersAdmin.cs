using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.OrdersAdmin
{
    public class GetOrdersAdmin
    {
        private IOrderService OrderService { get; }


        public GetOrdersAdmin(IOrderService orderService)
        {
            OrderService = orderService;
        }

        public IEnumerable<Response> Do(int status)
        {
            return OrderService.GetOrdersByStatus((OrderStatus)status, o => new Response()
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
