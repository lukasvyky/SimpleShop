using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.User.Orders;

namespace Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        public GetOrder.Response Order { get; set; }

        public void OnGet([FromServices] GetOrder getOrder, string reference)
        {
            Order = getOrder.Do(reference);
        }
    }
}
