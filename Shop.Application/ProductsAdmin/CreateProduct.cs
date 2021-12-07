using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext Context { get; }
        public CreateProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task Do(ProductViewModel vm)
        {
            Context.Products.Add(new Product
            {
                Value = vm.Value,
                Name = vm.Description,
                Description = vm.Description
            });
            await Context.SaveChangesAsync();
        }
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
