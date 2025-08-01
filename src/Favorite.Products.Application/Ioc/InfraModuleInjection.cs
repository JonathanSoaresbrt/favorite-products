using Favorite.Products.Application.Interfaces.Services;
using Favorite.Products.Application.Ioc.Interfaces;
using Favorite.Products.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Favorite.Products.Application.Ioc
{
    public class InfraModuleInjection : IApplicationModuleInjection
    {
        public void RegisterServices(IServiceCollection services)
        {
            #region Services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFavoriteProductService, FavoriteProductService>();
            services.AddScoped<IFavoriteProductService, FavoriteProductService>();
            services.AddScoped<IExternalProductService, ExternalProductService>();
            services.AddScoped<ITokenService, TokenService>();
            #endregion
        }
    }
}