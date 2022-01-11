using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database
{
    public class StockService : IStockService
    {
        private ApplicationDbContext Context { get; }
        public StockService(ApplicationDbContext context)
        {
            Context = context;
        }

        public Task<int> CreateStock(Stock stock)
        {
            Context.Stocks.Add(stock);
            return Context.SaveChangesAsync();
        }

        public Task<int> UpdateStockRange(IEnumerable<Stock> stocks)
        {
            Context.UpdateRange(stocks);
            return Context.SaveChangesAsync();
        }

        public Task<int> UpdateStock(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteStock(int id)
        {
            var stockToRemove = Context.Stocks.Find(id);
            Context.Stocks.Remove(stockToRemove);
            return Context.SaveChangesAsync();
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

        public Task RetrieveExpiredStockOnHold()
        {
            var stocksOnHold = Context.StockOnHold.Where(s => s.ExpiryDate < DateTime.Now).ToList();

            if (stocksOnHold.Any())
            {
                var stockToReturn = Context.Stocks.AsEnumerable().Where(s => stocksOnHold.Any(sh => sh.StockId == s.Id));

                foreach (var stock in stockToReturn)
                {
                    stock.Qty += stocksOnHold.FirstOrDefault(s => s.StockId == stock.Id).Qty;
                }

                Context.StockOnHold.RemoveRange(stocksOnHold);
                return Context.SaveChangesAsync();
            }

            return Task.CompletedTask;
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
