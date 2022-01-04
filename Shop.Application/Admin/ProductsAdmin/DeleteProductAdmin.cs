using Shop.Database;

namespace Shop.Application.Admin.ProductsAdmin
{
    public class DeleteProductAdmin
    {
        private ApplicationDbContext Context { get; }
        public DeleteProductAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<bool> Do(int id)
        {
            var productToDelete = Context.Products.Find(id);
            Context.Products.Remove(productToDelete);
            await Context.SaveChangesAsync();

            return true;
        }
    }
}
