using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class GetStock
    {
        private ApplicationDbContext Context { get; }
        public GetStock(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            var stock = Context.Products
                .Include(p => p.Stock)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Stock = p.Stock.Select(x => new StockViewModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Qty = x.Qty
                    })
                })
                .ToList();
        
            return stock;
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }


}
