using ANCIA.Authentication.Domain.Models;

namespace ANCIA.Authentication.Application.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<ClaimDto> Claims { get; set; }

        public static explicit operator UserDto(AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Active = user.Active,
                Roles = new List<string>(),
                Claims = new List<ClaimDto>()
            };
        }
    }
}
