using Favorite.Products.Application.Dtos;

namespace Favorite.Products.Application.Interfaces.Services
{
   public interface IExternalProductService
{
    Task<ProductDto?> GetProductByIdAsync(long id);
}
}