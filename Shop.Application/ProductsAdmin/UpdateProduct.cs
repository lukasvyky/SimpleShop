using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext Context { get; }
        public UpdateProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task Do(ProductViewModel vm) => await Context.SaveChangesAsync();

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
