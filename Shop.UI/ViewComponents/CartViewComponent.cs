using Microsoft.AspNetCore.Mvc;
using Shop.Application.User.Cart;

namespace Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private GetCart GetCart { get; }

        public CartViewComponent(GetCart getCart)
        {
            GetCart = getCart;
        }
        public IViewComponentResult Invoke(string view = "Default")
        {

            return View(view, GetCart.Do());
        }
    }
}
