using Setsis.Core.Dtos;
using Setsis.Service.Services.User.Dto.Request;
using Setsis.Service.Services.User.Dto.Result;

namespace Setsis.Service.Services.User
{
    public interface IUserService
    {
        Task<Response<ApplicationUserDto>> CreateUserAsync(CreateUserRequest createUserDto);
    }
}
