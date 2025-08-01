using Favorite.Products.Application.Dtos;
using Favorite.Products.Domain.Models.Entities;

namespace Favorite.Products.Application.Interfaces.Services
{
    public interface IFavoriteProductService
    {
        Task<List<ProductDto>> GetFavoritesByCustomerAsync(long customerId);
        Task AddAsync(FavoriteProduct favoriteProduct);
        Task RemoveAsync(long customerId, long productId);
    }
}
