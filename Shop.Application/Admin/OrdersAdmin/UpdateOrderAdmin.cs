using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.OrdersAdmin
{
    [Service]
    public class UpdateOrderAdmin
    {
        private IOrderService OrderService { get; }

        public UpdateOrderAdmin(IOrderService orderService)
        {
            OrderService = orderService;
        }

        public Task<int> Do(int id)
        {
            return OrderService.AdvanceOrder(id);
        }
    }
}
