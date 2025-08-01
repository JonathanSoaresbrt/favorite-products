using Favorite.Products.Application.Interfaces.Repositoryes;
using Favorite.Products.Infra.Data;
using Favorite.Products.Infra.Ioc.Interfaces;
using Favorite.Products.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Favorite.Products.Infra.Data.ConnectionSettings;
using Favorite.Products.Infra.Constants;

namespace Favorite.Products.Infra.Ioc
{
    public class InfraModuleInjection : IInfraModuleInjectionInfra
    {
        public void RegisterServices(IServiceCollection services)
        {
            #region DbContext
            services.AddDbContext<FavoriteProductsDbContext>((serviceProvider, options) =>
                {
                    var configuration = DatabaseConfiguration.GetConfiguration();
                    var connectionString = configuration.GetConnectionString(InfraConstants.ConnectionString);

                    options.UseNpgsql(connectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.CommandTimeout(1000);
                    });
                });
            #endregion

            #region Services
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
            #endregion
        }
    }
}