using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Shop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private SignInManager<IdentityUser> SignInManager { get; }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public async Task<IActionResult> OnPost()
        {
            var signResult = await SignInManager.PasswordSignInAsync(Input.Username, Input.Password, false, false);
            if (!signResult.Succeeded)
            {
                return Page();
            }
            return RedirectToPage("/Admin/Index");
        }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
