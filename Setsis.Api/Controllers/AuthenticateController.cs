using Microsoft.AspNetCore.Mvc;

using Setsis.Service.Services.Authenticate;
using Setsis.Service.Services.Authenticate.Dto.Request;

namespace Setsis.Api.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : CustomBaseController
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return ActionResultInstance(await _authenticateService.LoginAsync(request));
        }        

        [HttpPost]
        [Route("refresh-token-login")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            return ActionResultInstance(await _authenticateService.CreateTokenByRefreshToken(request.Token));
        }
    }
}
