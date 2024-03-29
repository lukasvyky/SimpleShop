﻿using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Application.User.Orders
{
    [Service]
    public class CreateOrder
    {
        private IOrderService OrderService { get; }
        private IStockService StockService { get; }

        public CreateOrder(IOrderService orderService, IStockService stockService)
        {
            OrderService = orderService;
            StockService = stockService;
        }

        public async Task<bool> Do(Request request)
        {
            var order = new Order
            {
                StripeReference = request.StripeReference,
                OrderRef = CreateOrderReference(),

                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode,

                OrderStocks = request.Stocks.Select(p => new OrderStock
                {
                    StockId = p.StockId,
                    Qty = p.Qty
                }).ToList()
            };

            var isSuccess = await OrderService.CreateOrder(order) > 0;

            if (!isSuccess)
            {
                return false;
            }

            await StockService.RemoveStockFromHold(request.SessionId);

            return true;
        }

        public class Request
        {
            public string SessionId { get; set; }
            public string StripeReference { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }


            public string Address { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        private string CreateOrderReference()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var value = new string(Enumerable.Repeat(chars, 12).Select(s => s[new Random().Next(s.Length)]).ToArray());
            return OrderService.DoesOrderRefExist(value) ? CreateOrderReference() : value;
        }
    }
}
