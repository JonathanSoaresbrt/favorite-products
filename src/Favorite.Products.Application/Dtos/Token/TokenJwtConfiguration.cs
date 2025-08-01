namespace Favorite.Products.Application.Dtos.Token
{
    public class TokenJwtConfiguration
    {
        public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int ExpirationHours { get; set; }
    }
}