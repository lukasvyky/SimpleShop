using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.UsersAdmin;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        public CreateUser CreateUserService { get; set; }
        public UsersController(CreateUser createUser)
        {
            CreateUserService = createUser;
        }

        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Request request)
        {
            await CreateUserService.Do(request);

            return Ok();
        }
    }
}
