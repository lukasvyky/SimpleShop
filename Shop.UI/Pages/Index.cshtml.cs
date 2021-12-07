﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public CreateProduct.ProductViewModel Product { get; set; }
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }
        private ApplicationDbContext Context { get; }

        public IndexModel(ApplicationDbContext context)
        {
            Context = context;
        }

        public void OnGet()
        {
            Products = new GetProducts(Context).Do();
        }

        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(Context).Do(Product);

            return RedirectToPage("Index");
        }
    }
}