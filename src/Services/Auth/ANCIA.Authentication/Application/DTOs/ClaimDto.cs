using System.Security.Claims;

namespace ANCIA.Authentication.Application.DTOs
{
    public class ClaimDto
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static explicit operator ClaimDto(Claim claim)
        {
            return new ClaimDto
            {
                Name = claim.Type,
                Value = claim.Value
            };
        }
    }
}
