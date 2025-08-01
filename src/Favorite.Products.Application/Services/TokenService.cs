using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Favorite.Products.Application.Dtos.Token;
using Favorite.Products.Application.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace Favorite.Products.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenJwtConfiguration _jwtConfig;

        public TokenService(TokenJwtConfiguration jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string GenerateToken(string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpirationHours),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
