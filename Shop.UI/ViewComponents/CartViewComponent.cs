using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private ApplicationDbContext Context { get; }

        public CartViewComponent(ApplicationDbContext context)
        {
            Context = context;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view,new GetCart(HttpContext.Session, Context).Do());
        }
    }
}
