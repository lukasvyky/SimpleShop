using Microsoft.EntityFrameworkCore;
using Shop.Database;

namespace Shop.Application.OrdersAdmin
{
    public class GetOrder
    {
        private ApplicationDbContext Context { get; }

        public GetOrder(ApplicationDbContext context)
        {
            Context = context;
        }

        public Response Do(int id)
        {
            return Context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderStocks)
                .ThenInclude(os => os.Stock)
                .ThenInclude(s => s.Product)
                .Select(o => new Response
                {
                    Id = o.Id,
                    StripeReference = o.StripeReference,
                    OrderRef = o.OrderRef,

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
                        Qty = os.Stock.Qty,
                        StockDescription = os.Stock.Description
                    })
                })
                .FirstOrDefault();
        }
        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string StripeReference { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public IEnumerable<Product> Products { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
            public string StockDescription { get; set; }
        }
    }
}
