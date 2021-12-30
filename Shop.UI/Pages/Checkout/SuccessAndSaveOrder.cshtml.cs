using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Database;
using Stripe;

namespace Shop.UI.Pages.Checkout
{
    public class SuccessAndSaveOrderModel : PageModel
    {
        private ApplicationDbContext Context { get; }
        private IConfiguration Config{ get; }

        public SuccessAndSaveOrderModel(ApplicationDbContext context, IConfiguration config)
        {
            Context = context;
            Config = config;
        }
        public async Task OnGet([FromQuery(Name = "payment_intent")] string paymentIntentId)
        {
            StripeConfiguration.ApiKey = Config.GetSection("Stripe")["SecretKey"];
            var cartOrder = new GetOrder(HttpContext.Session, Context).Do();

            var paymentIntent = await new PaymentIntentService().GetAsync(paymentIntentId);

            await new CreateOrder(Context).Do(new CreateOrder.Request()
            {
                StripeReference = paymentIntent.Charges.FirstOrDefault()?.Id,

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
        }
    }
}
