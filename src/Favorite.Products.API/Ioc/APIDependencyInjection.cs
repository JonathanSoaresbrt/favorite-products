using Favorite.Products.Infra.Ioc;
using Favorite.Products.Application.Ioc;
using Favorite.Products.API.Extensions;
using Favorite.Products.Application.Dtos.Token;

namespace Favorite.Products.API.Ioc
{
    public static class APIDependencyInjection
    {
        public static void RegisterDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterInfraServices();
            services.RegisterApplicationServices();
            services.AddSingleton(configuration.GetSection("JwtSettings").Get<TokenJwtConfiguration>());
        }
    }
}