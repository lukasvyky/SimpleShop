using Microsoft.AspNetCore.Http;
using Shop.Application.Infrastructure;
using Shop.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Shop.UI.Infrastructure
{
    public class SessionService : ISessionService
    {
        private ISession Session { get; }

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            Session = httpContextAccessor.HttpContext.Session;
        }

        public string GetId() => Session.Id;

        public void AddProduct(int stockId, int qty)
        {
            var hasCookieValue = Session.TryGetValue("cart", out byte[] value);
            var cartItems = new List<CartProduct>();

            if (hasCookieValue)
            {
                cartItems = JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(value));
            }

            if (cartItems.Any(cp => cp.StockId == stockId))
            {
                cartItems.Find(cp => cp.StockId == stockId).Qty += qty;
            }
            else
            {
                cartItems.Add(new CartProduct()
                {
                    StockId = stockId,
                    Qty = qty
                });
            }

            var stringObject = JsonSerializer.Serialize(cartItems);

            Session.Set("cart", Encoding.UTF8.GetBytes(stringObject));
        }

        public void RemoveProduct(int stockId, int qty)
        {
            var doesCartExist = Session.TryGetValue("cart", out byte[] value);
            var cartItems = doesCartExist
                ? JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(value))
                : new List<CartProduct>();

            if (cartItems.Any(cp => cp.StockId == stockId))
            {
                var product = cartItems.Find(cp => cp.StockId == stockId);
                product.Qty -= qty;
                if (product.Qty <= 0)
                {
                    cartItems.Remove(product);
                }
            }

            var itemsToCart = JsonSerializer.Serialize(cartItems);

            Session.Set("cart", Encoding.UTF8.GetBytes(itemsToCart));
        }

        public List<CartProduct> GetCart()
        {
            var hasCookieValue = Session.TryGetValue("cart", out var value);

            if (!hasCookieValue)
            {
                return null;
            }

            var cartItems = JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(value));

            return cartItems;
        }

        public void AddCustomerInformation(CustomerInformation customer)
        {
            var customerAsText = JsonSerializer.Serialize(customer);

            Session.Set("customer-information", Encoding.UTF8.GetBytes(customerAsText));
        }

        public CustomerInformation GetCustomerInformation()
        {
            var hasCookieValue = Session.TryGetValue("customer-information", out byte[] value);
            return hasCookieValue
                ? JsonSerializer.Deserialize<CustomerInformation>(Encoding.ASCII.GetString(value))
                : null;
        }
    }
}