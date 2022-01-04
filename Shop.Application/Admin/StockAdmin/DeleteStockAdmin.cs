using Shop.Database;

namespace Shop.Application.Admin.StockAdmin
{
    public class DeleteStockAdmin
    {
        private ApplicationDbContext Context { get; }
        public DeleteStockAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<bool> Do(int id)
        {
            var stockToRemove = Context.Stocks.Find(id);
            Context.Stocks.Remove(stockToRemove);
            await Context.SaveChangesAsync();

            return true;
        }
    }
}
