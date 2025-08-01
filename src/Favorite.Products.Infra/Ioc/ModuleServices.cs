using Favorite.Products.Infra.Constants;
using Favorite.Products.Infra.Ioc.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Favorite.Products.Infra.Ioc
{
    public static class ModuleServices
    {
        public static void RegisterInfraServices(this IServiceCollection services)
        {
            var modules = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                                                 .Where(x => typeof(IInfraModuleInjectionInfra).IsAssignableFrom(x) && x.Name != InfraConstants.ModuleInjectionInterface)
                                                 .Select(x => x).ToList();
            foreach (var item in modules)
            {
                var iinjection = (IInfraModuleInjectionInfra?)Activator.CreateInstance(item);
                iinjection?.RegisterServices(services);
            }
        }
    }
}