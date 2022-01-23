using ANCIA.Authentication.Application.AuthToken;
using ANCIA.Authentication.Domain.Models;
using ANCIA.Authentication.Domain.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache _distributedCache;

        public TokenManager(IOptions<TokenRules> tokenRules, UserManager<AppUser> userManager, IDistributedCache distributedCache)
        {
            _tokenRules = tokenRules.Value;
            _userManager = userManager;
            _distributedCache = distributedCache;
        }

        #region JWT
        public async Task<string> CreateToken(AppUser user)
        {
            var expiration = GetTokenExpiration();
            var claims = GetDefaultClaims(user, expiration);
            await AddUserRolesClaims(user, claims);
            var tokenDescriptor = GetTokenDescriptor(claims);
            tokenDescriptor.Audience = _tokenRules.Audience;
            return CreateToken(tokenDescriptor);
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var tokenDescriptor = GetTokenDescriptor(claims);
            return CreateToken(tokenDescriptor);
        }

        private SecurityTokenDescriptor GetTokenDescriptor(IEnumerable<Claim> claims)
        {
            var now = DateTime.Now;
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _tokenRules.Issuer,
                Expires = GetTokenExpiration(),
                SigningCredentials = GetCredentials(_tokenRules.Secret),
                IssuedAt = now,
                NotBefore = now
            };
        }

        private DateTime GetTokenExpiration()
        {
            return DateTime.UtcNow.AddMinutes(_tokenRules.ExpirationInMinutes);
        }

        private IList<Claim> GetDefaultClaims(AppUser user, DateTime expiration)
        {
            return new List<Claim>() {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
        }

        private byte[] GetKey(string secret)
        {
            return Encoding.UTF8.GetBytes(secret);
        }

        private string CreateToken(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SigningCredentials GetCredentials(string secret)
        {
            return new SigningCredentials(new SymmetricSecurityKey(GetKey(secret)), SecurityAlgorithms.HmacSha256Signature);
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
        #endregion

        #region RefreshToken
        public string CreateRefreshToken()
        {
            long hashTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            return $"{Guid.NewGuid()}-{hashTime}".Replace("-", "");
        }

        public async Task SaveRefreshToken(string userId, string token)
        {
            var configs = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_tokenRules.ExpirationInMinutes) };
            await _distributedCache.SetStringAsync(userId, token, configs);
        }

        public async Task<bool> IsRefreshTokenValid(Guid userid, string refreshToken)
        {
            string databaseRefreshToken = await _distributedCache.GetStringAsync(userid.ToString());
            return databaseRefreshToken.Equals(refreshToken);
        }
        #endregion
    }
}
