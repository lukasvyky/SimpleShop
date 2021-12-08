using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private ApplicationDbContext Context { get; }
        public GetProducts(ApplicationDbContext context)
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
