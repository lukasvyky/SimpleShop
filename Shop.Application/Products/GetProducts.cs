using Shop.Database;

namespace Shop.Application.Products
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
                Name = p.Name,
                Description = p.Description,
                Value = p.Value
            });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
