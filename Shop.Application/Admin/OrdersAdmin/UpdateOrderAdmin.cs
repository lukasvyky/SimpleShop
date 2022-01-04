using Shop.Database;

namespace Shop.Application.Admin.OrdersAdmin
{
    public class UpdateOrderAdmin
    {
        private ApplicationDbContext Context { get; }

        public UpdateOrderAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<bool> Do(int id)
        {
            var order = Context.Orders.FirstOrDefault(o => o.Id == id);

            order.OrderStatus += 1;

            return await Context.SaveChangesAsync() > 0;
        }
    }
}
