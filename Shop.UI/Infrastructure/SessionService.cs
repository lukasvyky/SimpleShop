﻿using Microsoft.AspNetCore.Http;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Shop.UI.Infrastructure
{
    public class SessionService : ISessionService
    {
        private ISession Session { get; }
        private const string keyCart = "Cart";
        private const string keyCustomerInfo = "customer-information";

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            Session = httpContextAccessor.HttpContext.Session;
        }

        public string GetId() => Session.Id;

        public void AddProduct(CartProduct cartProduct)
        {
            var hasCookieValue = Session.TryGetValue(keyCart, out byte[] value);
            var cartItems = new List<CartProduct>();

            if (hasCookieValue)
            {
                cartItems = JsonSerializer.Deserialize<List<CartProduct>>(Encoding.ASCII.GetString(value));
            }

            if (cartItems.Any(cp => cp.StockId == cartProduct.StockId))
            {
                cartItems.Find(cp => cp.StockId == cartProduct.StockId).Qty += cartProduct.Qty;
            }
            else
            {
                cartItems.Add(cartProduct);
            }

            var stringObject = JsonSerializer.Serialize(cartItems);

            Session.Set(keyCart, Encoding.UTF8.GetBytes(stringObject));
        }

        public void RemoveProduct(int stockId, int qty)
        {
            var doesCartExist = Session.TryGetValue(keyCart, out byte[] value);
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

            Session.Set(keyCart, Encoding.UTF8.GetBytes(itemsToCart));
        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var hasCookieValue = Session.TryGetValue(keyCart, out var value);

            if (!hasCookieValue)
            {
                return new List<TResult>();
            }

            var cartItems = JsonSerializer.Deserialize<IEnumerable<CartProduct>>(Encoding.ASCII.GetString(value));

            return cartItems.Select(selector);
        }

        public void ClearCart()
        {
            Session.Remove(keyCart);
        }

        public void AddCustomerInformation(CustomerInformation customer)
        {
            var customerAsText = JsonSerializer.Serialize(customer);

            Session.Set(keyCustomerInfo, Encoding.UTF8.GetBytes(customerAsText));
        }

        public CustomerInformation GetCustomerInformation()
        {
            var hasCookieValue = Session.TryGetValue(keyCustomerInfo, out byte[] value);
            return hasCookieValue
                ? JsonSerializer.Deserialize<CustomerInformation>(Encoding.ASCII.GetString(value))
                : null;
        }
    }
}