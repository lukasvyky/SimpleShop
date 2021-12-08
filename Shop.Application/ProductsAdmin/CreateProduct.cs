using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext Context { get; }
        public CreateProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var newProduct = new Product
            {
                Value = request.Value,
                Name = request.Description,
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
