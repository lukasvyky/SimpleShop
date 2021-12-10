using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products
{
    public class GetProduct
    {

        private ApplicationDbContext Context { get; }
        public GetProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public ProductViewModel Do(string productName)
        {
            return Context.Products
                    .Include(p => p.Stock)
                    .Where(p => p.Name.Equals(productName))
                    .Select(p => new ProductViewModel

                    {
                        Name = p.Name,
                        Description = p.Description,
                        Value = p.Value,
                        Stock = p.Stock.Select(s => new StockViewModel()
                        {
                            Id = s.Id,
                            Description = s.Description,
                            InStock = s.Qty > 0
                        })
                    })
                    .FirstOrDefault();
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }

            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
    }
}
