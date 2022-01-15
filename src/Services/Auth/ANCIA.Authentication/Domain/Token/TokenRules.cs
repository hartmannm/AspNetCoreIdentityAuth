namespace ANCIA.Authentication.Domain.Token
{
    public class TokenRules
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string Audience { get; set; }
        public int ExpirationInHours { get; set; }
    }
}
