using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database
{
    public class ProductService : IProductService
    {
        private ApplicationDbContext Context { get; }

        public ProductService(ApplicationDbContext applicationDbContext)
        {
            Context = applicationDbContext;
        }
        public TResult GetProductByName<TResult>(string productName, Func<Product, TResult> selector)
        {
            return Context.Products
                .Include(p => p.Stock)
                .Where(p => p.Name.Equals(productName))
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector)
        {
            return Context.Products
                  .Where(p => p.Id == id)
                  .Select(selector)
                  .FirstOrDefault();
        }

        public IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector)
        {
            return Context.Products
                .Include(p => p.Stock)
                .Select(selector)
                .ToList();
        }

        public Task<int> CreateProduct(Product product)
        {
            Context.Products.Add(product);

            return Context.SaveChangesAsync();
        }

        public Task<int> UpdateProduct(Product product)
        {
            Context.Products.Update(product);
            return Context.SaveChangesAsync();
        }

        public Task<int> DeleteProduct(int id)
        {
            var productToDelete = Context.Products.Find(id);
            Context.Products.Remove(productToDelete);
            return Context.SaveChangesAsync();
        }
    }
}
