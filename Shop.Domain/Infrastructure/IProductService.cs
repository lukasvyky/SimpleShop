using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public interface IProductService
    {
        TResult GetProductByName<TResult>(string productName, Func<Product, TResult> selector);
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);

        Task<int> CreateProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<int> DeleteProduct(int id);
    }
}
