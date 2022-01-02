using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Database;
using Stripe;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private IConfiguration Config { get; }
        private ApplicationDbContext Context { get; }
        public string ClientSecret { get; set; }

        public PaymentModel(IConfiguration config, ApplicationDbContext context)
        {
            Config = config;
            Context = Context;
        }
        public async Task<IActionResult> OnGet()
        {
            var customerIntel = new GetCustomerInformation(HttpContext.Session).Do();

            if (customerIntel is null)
            {
                return RedirectToPage("CustomerInformation");
            }

            var cartOrder = new Shop.Application.Cart.GetOrder(HttpContext.Session, Context).Do();

            StripeConfiguration.ApiKey = Config.GetSection("Stripe")["SecretKey"];

            var paymentIntentService = new PaymentIntentService();

            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Description = "My cool Purchase",
                Amount = cartOrder.GetTotalCharge(),
                Currency = "czk",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                }
            });
            ClientSecret = paymentIntent.ClientSecret;

            return Page();
        }
    }
}
