using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.User.Cart;
using System.Collections.Generic;

namespace Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<GetCart.Response> CartItems { get; set; }

        public IActionResult OnGet([FromServices] GetCart getCart)
        {
            CartItems = getCart.Do();

            return Page();
        }
    }
}
