using Favorite.Products.API.Configuration.Swagger;
using Favorite.Products.API.Middleware;
using Favorite.Products.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Favorite.Products.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseFavoriteProductsVersioning();
        }

        public static async Task ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FavoriteProductsDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
