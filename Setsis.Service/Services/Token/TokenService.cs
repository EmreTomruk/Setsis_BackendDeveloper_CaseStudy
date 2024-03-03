using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Setsis.Core.Configurations;
using System.Security.Claims;
using Setsis.Core.Models;
using Setsis.Service.Services.Token.Dto;

namespace Setsis.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOption _tokenOption;

        public TokenService(IOptions<CustomTokenOption> options)
        {
            _tokenOption = options.Value;
        }

        public TokenDto CreateToken(ApplicationUser user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.TokenValidityInMinutes);
            var refreshTokenExpiration = DateTime.Now.AddDays(_tokenOption.RefreshTokenValidityInDays);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.Secret));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(
                 issuer: _tokenOption.ValidIssuer,
                 audience: _tokenOption.ValidAudience,
                 expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: claims,
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }


        #region Private Methods

        private static string CreateRefreshToken()
        {
            var numberByte = new byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        #endregion
    }
}
