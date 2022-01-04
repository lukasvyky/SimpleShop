using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.User.Cart;
using Shop.Application.User.Products;

namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        public GetProduct.ProductViewModel Product { get; set; }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public async Task<IActionResult> OnGet([FromServices] GetProduct getProduct, string name)
        {
            var incomingProduct = await getProduct.Do(name.Replace('-', ' '));
            if (incomingProduct is null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                Product = incomingProduct;
                return Page();
            }
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var wasStockAdded = await addToCart.Do(CartViewModel);

            if (!wasStockAdded)
            {
                //TODO: add a warning
                return Page();
            }

            return RedirectToPage("Cart");
        }
    }
}
