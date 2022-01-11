using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.StockAdmin
{
    public class DeleteStockAdmin
    {
        private IStockService StockService { get; }

        public DeleteStockAdmin(IStockService stockService)
        {
            StockService = stockService;
        }

        public Task<int> Do(int id)
        {
            return StockService.DeleteStock(id);
        }
    }
}
