using Shop.Database;

namespace Shop.Application.Admin.ProductsAdmin
{
    public class GetProductsAdmin
    {
        private ApplicationDbContext Context { get; }
        public GetProductsAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<ProductViewModel> Do() =>
            Context.Products.ToList().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Value = p.Value
            });
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
    }
}
