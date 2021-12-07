using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private ApplicationDbContext Context { get; }
        public DeleteProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task Do(int id)
        {
            var productToDelete = Context.Products.Find(id);
            Context.Products.Remove(productToDelete);
            await Context.SaveChangesAsync();
        }
    }
}
