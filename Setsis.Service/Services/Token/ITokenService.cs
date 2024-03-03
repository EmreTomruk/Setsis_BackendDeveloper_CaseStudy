using Setsis.Core.Models;
using Setsis.Service.Services.Token.Dto;

namespace Setsis.Service.Services.Token
{
    public interface ITokenService
    {
        TokenDto CreateToken(ApplicationUser user);
    }
}
