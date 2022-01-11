using Shop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public interface IStockService
    {
        Task<int> CreateStock(Stock stock);
        Task<int> UpdateStockRange(IEnumerable<Stock> stocks);
        Task<int> DeleteStock(int id);

        Stock GetStockWithProduct(int stockId);
        bool IsThereEnoughStock(int stockId, int qty);
        Task PutStockOnHold(int stockId, int qty, string sessionId);
        Task RemoveStockFromHold(int stockId, int qty, string sessionId);
        Task RemoveStockFromHold(string sessionId);

        Task RetrieveExpiredStockOnHold();
    }
}
