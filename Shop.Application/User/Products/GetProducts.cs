using Shop.Database;

namespace Shop.Application.User.Products
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
                Value = $"CZK{p.Value.ToString("N2")}"
            });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
        }
    }
}
