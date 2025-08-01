namespace Favorite.Products.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(string email, string role);
    }
}