using Favorite.Products.Application.Interfaces.Repositoryes;
using Favorite.Products.Domain.Models.Entities;
using Favorite.Products.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Favorite.Products.Infra.Repositories
{
    public class FavoriteProductRepository : IFavoriteProductRepository
    {
        private readonly FavoriteProductsDbContext _context;

        public FavoriteProductRepository(FavoriteProductsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FavoriteProduct favoriteProduct)
        {
            await _context.FavoriteProducts.AddAsync(favoriteProduct);
        }

        public void Update(FavoriteProduct favoriteProduct)
        {
            _context.FavoriteProducts.Update(favoriteProduct);
        }

        public async Task<FavoriteProduct?> GetByCustomerAndProductIdAsync(long customerId, long productId)
        {
            return await _context.FavoriteProducts
                .FirstOrDefaultAsync(fp => fp.CustomerId == customerId && fp.ProductId == productId);
        }

        public async Task<List<FavoriteProduct>> GetByCustomerIdAsync(long customerId)
        {
            return await _context.FavoriteProducts
                .Where(fp => fp.CustomerId == customerId &&
                             fp.Inactive == false &&
                             fp.Customer.Inactive == false)
                .ToListAsync();
        }


        public void Remove(FavoriteProduct favoriteProduct)
        {
            _context.FavoriteProducts.Remove(favoriteProduct);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
