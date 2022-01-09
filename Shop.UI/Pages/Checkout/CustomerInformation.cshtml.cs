using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Shop.Application.User.Cart;

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

        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            var customerIntel = getCustomerInformation.Do();

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

            return RedirectToPage("Payment");
        }

        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            addCustomerInformation.Do(CustomerInformation);

            return RedirectToPage("Payment");
        }
    }
}
