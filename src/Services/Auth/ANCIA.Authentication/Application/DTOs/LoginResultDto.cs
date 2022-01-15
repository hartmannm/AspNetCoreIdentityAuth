namespace ANCIA.Authentication.Application.DTOs
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }

        public LoginResultDto(string token, string userId, string userEmail)
        {
            Token = token;
            UserId = userId;
            UserEmail = userEmail;
        }
    }
}
