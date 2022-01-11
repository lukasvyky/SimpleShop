using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Database
{
    public class StockService : IStockService
    {
        private ApplicationDbContext Context { get; }
        public StockService(ApplicationDbContext context)
        {
            Context = context;
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return Context.Stocks.Where(s => s.Id == stockId)
                .Include(s => s.Product)
                .FirstOrDefault();
        }

        public bool IsThereEnoughStock(int stockId, int qty)
        {
            return Context.Stocks.FirstOrDefault(s => s.Id == stockId).Qty >= qty;
        }

        public Task PutStockOnHold(int stockId, int qty, string sessionId)
        {
            Context.Stocks.FirstOrDefault(s => s.Id == stockId).Qty -= qty;

            var stocksOnHold = Context.StockOnHold.Where(s => s.SessionId == sessionId).ToList();
            if (stocksOnHold.Any(s => s.StockId == stockId))
            {
                stocksOnHold.FirstOrDefault(s => s.StockId == stockId).Qty += qty;
            }
            else
            {
                Context.StockOnHold.Add(new StockOnHold()
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Qty = qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });
            }
            Context.StockOnHold.Where(s => s.SessionId == sessionId).ToList().ForEach(s => s.ExpiryDate = DateTime.Now.AddMinutes(20));

            return Context.SaveChangesAsync();
        }
        public Task RemoveStockFromHold(string sessionId)
        {
            var stockOnHold = Context.StockOnHold.Where(s => s.SessionId == sessionId).ToList();
            Context.StockOnHold.RemoveRange(stockOnHold);

            return Context.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(int stockId, int qty, string sessionId)
        {

            var originalStock = Context.Stocks.Where(s => s.Id == stockId).FirstOrDefault();
            var stockOnHold = Context.StockOnHold.FirstOrDefault(s => s.StockId == stockId && s.SessionId == sessionId);

            originalStock.Qty += stockOnHold.Qty;
            stockOnHold.Qty -= qty;

            if (stockOnHold.Qty <= 0)
            {
                Context.StockOnHold.Remove(stockOnHold);
            }

            return Context.SaveChangesAsync();
        }
    }
}
