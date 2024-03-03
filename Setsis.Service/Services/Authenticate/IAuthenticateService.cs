using Setsis.Core.Dtos;
using Setsis.Service.Services.Authenticate.Dto.Request;
using Setsis.Service.Services.Token.Dto;

namespace Setsis.Service.Services.Authenticate
{
    public interface IAuthenticateService
    {
        Task<Response<TokenDto>> LoginAsync(LoginRequest userLoginRequest);
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
    }
}
