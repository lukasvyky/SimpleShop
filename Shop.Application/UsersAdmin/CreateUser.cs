using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Shop.Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> UserManager { get; }

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task<bool> Do(Request request)
        {
            var managerUser = new IdentityUser()
            {
                UserName = request.Username
            };

            await UserManager.CreateAsync(managerUser, "password");

            var managerClaim = new Claim("Role", "Manager");

            await UserManager.AddClaimAsync(managerUser, managerClaim);
            return true;
        }

        public class Request
        {
            public string Username { get; set; }
        }
    }
}
