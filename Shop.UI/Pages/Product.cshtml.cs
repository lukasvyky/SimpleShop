using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Products;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext Context { get; }
        public GetProduct.ProductViewModel Product { get; set; }

        public ProductModel(ApplicationDbContext context)
        {
            Context = context;
        }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public IActionResult OnGet(string name)
        {
            var incomingProduct = new GetProduct(Context).Do(name.Replace('-', ' '));
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

        public IActionResult OnPost()
        {
            new AddToCart(HttpContext.Session).Do(CartViewModel);

            return RedirectToPage("Cart");
        }
    }
}
