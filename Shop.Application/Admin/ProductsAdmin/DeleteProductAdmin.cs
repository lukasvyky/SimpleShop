using Shop.Domain.Infrastructure;

namespace Shop.Application.Admin.ProductsAdmin
{
    public class DeleteProductAdmin
    {
        private IProductService ProductService { get; }

        public DeleteProductAdmin(IProductService productService)
        {
            ProductService = productService;
        }

        public Task<int> Do(int id)
        {
            return ProductService.DeleteProduct(id);
        }
    }
}
