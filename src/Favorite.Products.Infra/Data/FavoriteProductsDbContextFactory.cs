using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Favorite.Products.Infra.Data.ConnectionSettings;
using Favorite.Products.Infra.Constants;

namespace Favorite.Products.Infra.Data
{
    public class FavoriteProductsDbContextFactory : IDesignTimeDbContextFactory<FavoriteProductsDbContext>
    {
        public FavoriteProductsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FavoriteProductsDbContext>();

            var connectionString = ConnectionStringGenerateMigration.GetConnectionStringMigration();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    InfraConstants.ExceptionFavoriteProductsDbContext);
            }

            builder.UseNpgsql(connectionString);

            return new FavoriteProductsDbContext(builder.Options);
        }
    }
}
