using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Logout([FromServices] SignInManager<IdentityUser> signInManager)
        {
            await signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
