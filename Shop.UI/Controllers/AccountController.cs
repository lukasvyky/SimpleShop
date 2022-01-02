using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            SignInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }
    }
}
