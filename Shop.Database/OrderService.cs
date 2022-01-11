using Microsoft.EntityFrameworkCore;
using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext Context { get; }

        public OrderService(ApplicationDbContext context)
        {
            Context = context;
        }

        public Task<int> AdvanceOrder(int id)
        {
            var order = Context.Orders.FirstOrDefault(o => o.Id == id).OrderStatus++;

            return Context.SaveChangesAsync();
        }

        public Task<int> CreateOrder(Order order)
        {
            Context.Orders.Add(order);

            return Context.SaveChangesAsync();
        }

        public bool DoesOrderRefExist(string reference)
        {
            return Context.Orders.Any(o => o.OrderRef.Equals(reference));
        }

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
        {
            return GetOrder(o => o.Id == id, selector);
        }

        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector)
        {
            return Context.Orders.Where(o => o.OrderStatus == status).Select(selector);
        }

        public TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector)
        {
            return GetOrder(o => o.OrderRef.Equals(reference), selector);
        }

        private TResult GetOrder<TResult>(Func<Order, bool> condition, Func<Order, TResult> selector)
        {
            return Context.Orders.Where(o => condition(o))
               .Include(o => o.OrderStocks)
               .ThenInclude(os => os.Stock)
               .ThenInclude(s => s.Product)
               .Select(selector)
               .FirstOrDefault();
        }
    }
}
