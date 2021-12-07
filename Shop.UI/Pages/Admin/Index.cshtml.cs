using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class AdminIndexModel : IndexModel
    {
        public AdminIndexModel(ApplicationDbContext context) : base(context)
        {
        }
    }
}