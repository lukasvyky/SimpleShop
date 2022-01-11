using Shop.Application;
using Shop.Database;
using Shop.Domain.Infrastructure;
using Shop.UI.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;
            var services = definedTypes.Where(t => t.GetTypeInfo().GetCustomAttribute<Service>() is not null);

            foreach (var type in services)
            {
                @this.AddTransient(type);
            }

            @this.AddScoped<ISessionService, SessionService>();
            @this.AddTransient<IProductService, ProductService>();
            @this.AddTransient<IStockService, StockService>();
            @this.AddTransient<IOrderService, OrderService>();

            @this.AddHttpContextAccessor();

            return @this;
        }
    }
}
