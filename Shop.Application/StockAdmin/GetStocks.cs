using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class GetStocks
    {
        private ApplicationDbContext Context { get; }
        public GetStocks(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<StockViewModel> Do(int productId)
        {
            var stocks = Context.Stocks.Where(p => p.Id == productId).
                Select(p => new StockViewModel 
                {
                    Id = p.Id,
                    Description = p.Description,
                    Qty = p.Qty
                }).
                ToList();

            return stocks;
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }


}
