using Shop.Database;

namespace Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private ApplicationDbContext Context { get; }
        public DeleteStock(ApplicationDbContext context)
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
