using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        public GetCart.Response Cart { get; set; }
        private ApplicationDbContext Context { get; }

        public CartModel(ApplicationDbContext context)
        {
            Context = context;
        }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, Context).Do();

            return Page();
        }
    }
}
