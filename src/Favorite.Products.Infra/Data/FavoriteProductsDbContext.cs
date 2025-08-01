using Favorite.Products.Domain.Models.Entities;
using Favorite.Products.Infra.Constants;
using Microsoft.EntityFrameworkCore;

namespace Favorite.Products.Infra.Data
{
    public class FavoriteProductsDbContext : DbContext
    {
        public FavoriteProductsDbContext(DbContextOptions<FavoriteProductsDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch(InfraConstants.NpgTime, true);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FavoriteProductsDbContext).Assembly);
        }
    }
}
