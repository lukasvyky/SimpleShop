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

        public async Task<IActionResult> OnGet(string name)
        {
            var incomingProduct = await new GetProduct(Context).Do(name.Replace('-', ' '));
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

        public async Task<IActionResult> OnPost()
        {
            var wasStockAdded = await new AddToCart(HttpContext.Session, Context).Do(CartViewModel);

            if (!wasStockAdded)
            {
                //TODO: add a warning
                return Page();
            }

            return RedirectToPage("Cart");
        }
    }
}
