using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Database;

namespace Shop.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        private ApplicationDbContext Context { get; }

        public UpdateOrder(ApplicationDbContext context)
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
