using Microsoft.Extensions.DependencyInjection;

namespace Favorite.Products.Application.Ioc.Interfaces
{
    public interface IApplicationModuleInjection
    {
        void RegisterServices(IServiceCollection services);
    }
}