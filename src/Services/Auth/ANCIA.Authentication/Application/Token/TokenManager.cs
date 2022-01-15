using ANCIA.Authentication.Application.AuthToken;
using ANCIA.Authentication.Domain.Models;
using ANCIA.Authentication.Domain.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ANCIA.Authentication.Application.Token
{
    public class TokenManager : ITokenManager
    {
        private readonly TokenRules _tokenRules;
        private readonly UserManager<AppUser> _userManager;

        public TokenManager(IOptions<TokenRules> tokenRules, UserManager<AppUser> userManager)
        {
            _tokenRules = tokenRules.Value;
            _userManager = userManager;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = GetDefaultClaims(user);
            await AddUserRolesClaims(user, claims);

            var key = Encoding.UTF8.GetBytes(_tokenRules.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _tokenRules.Issuer,
                Audience = _tokenRules.Audience,
                Expires = DateTime.UtcNow.AddHours(_tokenRules.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private IList<Claim> GetDefaultClaims(AppUser user)
        {
            return new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
            };
        }

        private async Task AddUserRolesClaims(AppUser user, IList<Claim> claimsList)
        {
            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claimsList.Add(new Claim("role", role));
            }

            foreach (var claim in await _userManager.GetClaimsAsync(user))
            {
                claimsList.Add(claim);
            }
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
