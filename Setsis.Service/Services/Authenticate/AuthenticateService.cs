using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Setsis.Core.Dtos;
using Setsis.Core.Models;
using Setsis.Core.UnitOfWork;
using Setsis.Core.Validation;
using Setsis.Infrastructure;
using Setsis.Service.Services.Authenticate.Validator;
using Setsis.Service.Services.Authenticate.Dto.Request;
using Setsis.Service.Services.Token;
using Setsis.Service.Services.Token.Dto;

namespace Setsis.Service.Services.Authenticate
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IValidationService _validationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork<SetsisDbContext> _unitOfWork;

        public AuthenticateService(UserManager<ApplicationUser> userManager, IValidationService validationService, ITokenService tokenService, IUnitOfWork<SetsisDbContext> unitOfWork)
        {
            _userManager = userManager;
            _validationService = validationService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<TokenDto>> LoginAsync(LoginRequest userLoginRequest)
        {
            var validationResult = _validationService.Validate(typeof(LoginValidator), userLoginRequest);

            if (!validationResult.IsValid)
                return Response<TokenDto>.Fail(new ErrorDto(validationResult.ErrorMessages), 400);

            var user = await _userManager.FindByEmailAsync(userLoginRequest.Email);

            if (user == null)
                return Response<TokenDto>.Fail("Email or Password is wrong", 400);

            if (!await _userManager.CheckPasswordAsync(user, userLoginRequest.Password))
                return Response<TokenDto>.Fail("Email or Password is wrong", 400);

            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _unitOfWork.GetRepository<UserRefreshToken>().Entities.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                var refreshToken = new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration };

                await _unitOfWork.GetRepository<UserRefreshToken>().AddAsync(refreshToken);
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommmitAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _unitOfWork.GetRepository<UserRefreshToken>().Entities.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
                return Response<TokenDto>.Fail("Refresh token not found", 404);

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
                return Response<TokenDto>.Fail("User not found", 404);

            var token = _tokenService.CreateToken(user);

            existRefreshToken.Code = token.RefreshToken;
            existRefreshToken.Expiration = token.RefreshTokenExpiration;

            await _unitOfWork.CommmitAsync();

            return Response<TokenDto>.Success(token, 200);
        }
    }
}
