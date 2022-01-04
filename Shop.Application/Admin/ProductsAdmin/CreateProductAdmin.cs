using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Admin.ProductsAdmin
{
    public class CreateProductAdmin
    {
        private ApplicationDbContext Context { get; }
        public CreateProductAdmin(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var newProduct = new Product
            {
                Value = request.Value,
                Name = request.Name,
                Description = request.Description
            };

            Context.Products.Add(newProduct);

            await Context.SaveChangesAsync();

            return new Response
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Value = newProduct.Value
            };
        }
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
