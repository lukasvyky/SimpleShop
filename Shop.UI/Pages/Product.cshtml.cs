using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    }
}
