using Microsoft.AspNetCore.Identity;
using Setsis.Core.Dtos;
using Setsis.Core.Models;
using Setsis.Core.Validation;
using Setsis.Service.Mapping;
using Setsis.Service.Services.User.Dto.Request;
using Setsis.Service.Services.User.Dto.Result;
using Setsis.Service.Services.User.Validator;

namespace Setsis.Service.Services.User
{
    public class UserService : IUserService
    {
        private readonly IValidationService _validationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager, IValidationService validationService)
        {
            _userManager = userManager;
            _validationService = validationService;
        }

        public async Task<Response<ApplicationUserDto>> CreateUserAsync(CreateUserRequest createUserDto)
        {
            var validationResult = _validationService.Validate(typeof(CreateUserValidator), createUserDto);

            if (!validationResult.IsValid)
                return Response<ApplicationUserDto>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            var user = new ApplicationUser { Email = createUserDto.Email, UserName = createUserDto.Username };

            var userExists = await _userManager.FindByNameAsync(createUserDto.Username);
            if (userExists != null)
                return Response<ApplicationUserDto>.Fail(new ErrorDto("User already exists!"), 500);

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return Response<ApplicationUserDto>.Fail(new ErrorDto(errors), 400);
            }

            return Response<ApplicationUserDto>.Success(ObjectMapper.Mapper.Map<ApplicationUserDto>(user), 200);
        }
    }
}
