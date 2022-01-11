using Shop.Domain.Enums;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public interface IOrderService
    {
        Task<int> CreateOrder(Order order);
        bool DoesOrderRefExist(string reference);
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector);
        Task<int> AdvanceOrder(int id);
    }
}
