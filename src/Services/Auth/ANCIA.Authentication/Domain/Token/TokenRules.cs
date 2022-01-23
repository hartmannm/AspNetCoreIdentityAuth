namespace ANCIA.Authentication.Domain.Token
{
    public class TokenRules
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string Audience { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string RefreshTokenHeader { get; set; }
        public string NewAuthTokenHeader { get; set; }
        public string NewRefreshTokenHeader { get; set; }
    }
}
