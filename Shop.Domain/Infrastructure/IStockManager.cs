using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Domain.Infrastructure
{
    public interface IStockService
    {
        Stock GetStockWithProduct(int stockId);
        bool IsThereEnoughStock(int stockId, int qty);
        Task PutStockOnHold(int stockId, int qty, string sessionId);
        Task RemoveStockFromHold(int stockId, int qty, string sessionId);
    }
}
