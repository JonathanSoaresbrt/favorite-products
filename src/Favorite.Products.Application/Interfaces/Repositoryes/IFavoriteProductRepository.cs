using Favorite.Products.Domain.Models.Entities;

namespace Favorite.Products.Application.Interfaces.Repositoryes
{
    public interface IFavoriteProductRepository
    {
        Task<List<FavoriteProduct>> GetByCustomerIdAsync(long customerId);
        Task<FavoriteProduct?> GetByCustomerAndProductIdAsync(long customerId, long productId);
        Task AddAsync(FavoriteProduct favoriteProduct);
        void Update(FavoriteProduct favoriteProduct);
        void Remove(FavoriteProduct favoriteProduct);
        Task SaveChangesAsync();
    }
}