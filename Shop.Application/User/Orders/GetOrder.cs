using Shop.Domain.Infrastructure;

namespace Shop.Application.User.Orders
{
    public class GetOrder
    {
        private IOrderService OrderService { get; }

        public GetOrder(IOrderService orderService)
        {
            OrderService = orderService;
        }

        public Response Do(string orderReference)
        {
            return OrderService.GetOrderByReference(orderReference, o => new Response
            {
                OrderRef = orderReference,

                FirstName = o.FirstName,
                LastName = o.LastName,
                Email = o.Email,
                PhoneNumber = o.PhoneNumber,
                Address = o.Address,
                Address2 = o.Address2,
                City = o.City,
                PostCode = o.PostCode,

                Products = o.OrderStocks.Select(os => new Product()
                {
                    Name = os.Stock.Product.Name,
                    Description = os.Stock.Product.Description,
                    Value = $"CZK {os.Stock.Product.Value.ToString("N2")}",
                    Qty = os.Stock.Qty,
                    StockDescription = os.Stock.Description
                }),

                TotalValue = o.OrderStocks.Sum(os => os.Stock.Product.Value).ToString("N2")
            });
        }
        public class Response
        {
            public string OrderRef { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public IEnumerable<Product> Products { get; set; }
            public string TotalValue { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public int Qty { get; set; }
            public string StockDescription { get; set; }
        }
    }
}
