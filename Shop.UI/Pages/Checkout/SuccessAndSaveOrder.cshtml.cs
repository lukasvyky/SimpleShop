using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.User.Cart;
using Shop.Application.User.Orders;
using Stripe;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Infrastructure;

namespace Shop.UI.Pages.Checkout
{
    public class SuccessAndSaveOrderModel : PageModel
    {
        public async Task OnGet(
            [FromQuery(Name = "payment_intent")] string paymentIntentId,
            [FromServices] GetCartOrder getCartOrder,
            [FromServices] CreateOrder createOrder,
            [FromServices] IConfiguration config,
            [FromServices] ISessionService sessionService
            )
        {
            StripeConfiguration.ApiKey = config.GetSection("Stripe")["SecretKey"];
            var cartOrder = getCartOrder.Do();

            var paymentIntent = await new PaymentIntentService().GetAsync(paymentIntentId);

            var isSuccess = await createOrder.Do(new CreateOrder.Request
            {
                StripeReference = paymentIntent.Charges.FirstOrDefault()?.Id,
                SessionId = HttpContext.Session.Id,

                FirstName = cartOrder.CustomerInformation.FirstName,
                LastName = cartOrder.CustomerInformation.LastName,
                Email = cartOrder.CustomerInformation.Email,
                PhoneNumber = cartOrder.CustomerInformation.PhoneNumber,
                Address = cartOrder.CustomerInformation.Address,
                Address2 = cartOrder.CustomerInformation.Address2,
                City = cartOrder.CustomerInformation.City,
                PostCode = cartOrder.CustomerInformation.PostCode,

                Stocks = cartOrder.Products.Select(p => new CreateOrder.Stock
                {
                    StockId = p.StockId,
                    Qty = p.Qty
                }).ToList()
            });

            if (isSuccess)
            {
                sessionService.ClearCart();
            }
        }
    }
}
