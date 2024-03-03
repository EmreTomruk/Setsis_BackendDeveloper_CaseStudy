using Microsoft.AspNetCore.Mvc;
using Setsis.Service.Services.User;
using Setsis.Service.Services.User.Dto.Request;

namespace Setsis.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(request));
        }
    }
}
