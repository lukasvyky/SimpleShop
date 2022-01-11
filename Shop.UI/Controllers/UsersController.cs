using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.UI.ViewModels;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> UserManager { get; }

        public UsersController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel vm)
        {
            var managerUser = new IdentityUser()
            {
                UserName = vm.Username
            };

            await UserManager.CreateAsync(managerUser, "password");

            var managerClaim = new Claim("Role", "Manager");

            await UserManager.AddClaimAsync(managerUser, managerClaim);

            return Ok();
        }
    }
}
