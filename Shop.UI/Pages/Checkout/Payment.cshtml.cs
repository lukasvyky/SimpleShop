using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.User.Cart;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string ClientSecret { get; set; }

        public async Task<IActionResult> OnGet(
            [FromServices] GetCustomerInformation getCustomerInformation,
            [FromServices] GetCartOrder getCartOrder,
            [FromServices] IConfiguration config
            )
        {
            var customerIntel = getCustomerInformation.Do();

            if (customerIntel is null)
            {
                return RedirectToPage("CustomerInformation");
            }

            var cartOrder = getCartOrder.Do();

            StripeConfiguration.ApiKey = config.GetSection("Stripe")["SecretKey"];

            var paymentIntentService = new PaymentIntentService();

            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Description = "My cool Purchase",
                Amount = cartOrder.GetTotalCharge(),
                Currency = "czk",
                PaymentMethodTypes = new List<string> { "card" }
            });
            ClientSecret = paymentIntent.ClientSecret;

            return Page();
        }
    }
}
