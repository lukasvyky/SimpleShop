using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }
        private IWebHostEnvironment Env { get; set; }

        public CustomerInformationModel(IWebHostEnvironment env)
        {
            Env = env;
        }

        public IActionResult OnGet()
        {
            var customerIntel = new GetCustomerInformation(HttpContext.Session).Do();

            if (customerIntel is null)
            {
                if (Env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request()
                    {
                        FirstName = "a",
                        LastName = "a",
                        Email = "a.b@seznam.cz",
                        PhoneNumber = "123456789",
                        Address = "a",
                        Address2 = "a",
                        City = "Praha",
                        PostCode = "a"
                    };
                }

                return Page();
            }
            else
            {
                return RedirectToPage("Payment");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);

            return RedirectToPage("Payment");
        }
    }
}
