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