using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        public GetOrder.Response Order { get; set; }
        private ApplicationDbContext Context { get; }

        public OrderModel(ApplicationDbContext context)
        {
            Context = context;
        }
        public void OnGet(string reference)
        {
            Order = new GetOrder(Context).Do(reference);
        }
    }
}
