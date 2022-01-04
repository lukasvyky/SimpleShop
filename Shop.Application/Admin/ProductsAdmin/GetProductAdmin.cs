using Shop.Database;

namespace Shop.Application.Admin.ProductsAdmin
{
    public class GetProductAdmin
    {
        private ApplicationDbContext Context { get; }
        public GetProductAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public ProductViewModel Do(int id) =>
            Context.Products.Where(p => p.Id == id).Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Value = p.Value
            })
            .FirstOrDefault();

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
