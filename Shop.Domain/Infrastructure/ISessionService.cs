using Shop.Domain.Models;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Infrastructure
{
    public interface ISessionService
    {
        string GetId();
        void AddProduct(CartProduct product);
        void RemoveProduct(int stockId, int qty);
        IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);
        void ClearCart();
        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();
    }
}
