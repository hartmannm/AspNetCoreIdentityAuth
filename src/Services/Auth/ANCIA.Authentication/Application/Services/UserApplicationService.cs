using ANCIA.Authentication.Application.DTOs;
using ANCIA.Authentication.Domain.Models;
using ANCIA.Core.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ANCIA.Authentication.Application.Services
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserApplicationService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ProcessResult<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                var dto = (UserDto)user;
                dto.Roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);

                dto.Claims = claims.ToList()
                    .Select(claim => (ClaimDto)claim)
                    .ToList();

                result.Add(dto);
            }


            return ProcessResult<IEnumerable<UserDto>>.Success(result);
        }
    }
}
