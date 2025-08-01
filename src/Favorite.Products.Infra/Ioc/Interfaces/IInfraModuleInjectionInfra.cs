using Microsoft.Extensions.DependencyInjection;

namespace Favorite.Products.Infra.Ioc.Interfaces
{
    public interface IInfraModuleInjectionInfra
    {
        void RegisterServices(IServiceCollection services);
    }
}