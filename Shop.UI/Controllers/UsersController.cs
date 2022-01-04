using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Admin.UsersAdmin;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        public async Task<IActionResult> CreateUser([FromServices] CreateUserAdmin createUser, [FromBody] CreateUserAdmin.Request request)
        {
            await createUser.Do(request);

            return Ok();
        }
    }
}
