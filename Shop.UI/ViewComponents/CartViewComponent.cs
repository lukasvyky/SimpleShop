using Microsoft.AspNetCore.Mvc;
using Shop.Application.User.Cart;
using System.Linq;

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
            if (view.Equals("Small"))
            {
                return View(view, $"CZK {GetCart.Do().Sum(i => i.RealValue * i.Qty)}");
            }
            return View(view, GetCart.Do());
        }
    }
}
