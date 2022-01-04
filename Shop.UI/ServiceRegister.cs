using Shop.Application;
using Shop.Application.Admin.OrdersAdmin;
using Shop.Application.Admin.ProductsAdmin;
using Shop.Application.Admin.StockAdmin;
using Shop.Application.Admin.UsersAdmin;
using Shop.Application.User.Cart;
using Shop.Application.User.Orders;
using Shop.Application.User.Products;
using Shop.UI;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddTransient<ISessionService, HttpContextGetter>();
            @this.AddHttpContextAccessor();

            RegisterAdminServices(@this);
            RegisterUserServices(@this);

            return @this;
        }

        private static void RegisterUserServices(IServiceCollection @this)
        {
            @this.AddTransient<AddCustomerInformation>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInformation>();
            @this.AddTransient<GetCartOrder>();

            @this.AddTransient<CreateOrder>();
            @this.AddTransient<GetOrder>();

            @this.AddTransient<CreateProduct>();
            @this.AddTransient<GetProduct>();
            @this.AddTransient<GetProducts>();
        }

        private static void RegisterAdminServices(IServiceCollection @this)
        {
            @this.AddTransient<CreateUserAdmin>();

            @this.AddTransient<GetOrderAdmin>();
            @this.AddTransient<GetOrdersAdmin>();
            @this.AddTransient<UpdateOrderAdmin>();

            @this.AddTransient<CreateProductAdmin>();
            @this.AddTransient<DeleteProductAdmin>();
            @this.AddTransient<GetProductAdmin>();
            @this.AddTransient<GetProductsAdmin>();
            @this.AddTransient<UpdateProductsAdmin>();

            @this.AddTransient<CreateStockAdmin>();
            @this.AddTransient<DeleteStockAdmin>();
            @this.AddTransient<GetStockAdmin>();
            @this.AddTransient<UpdateStockAdmin>();
        }
    }
}
