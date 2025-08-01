using Favorite.Products.Application.Constants;
using Favorite.Products.Application.Ioc.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Favorite.Products.Application.Ioc
{
    public static class ModuleServices
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            var modules = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                                                 .Where(x => typeof(IApplicationModuleInjection).IsAssignableFrom(x) && x.Name != ApplicationConstants.ModuleInterface)
                                                 .Select(x => x).ToList();
            foreach (var item in modules)
            {
                var iinjection = (IApplicationModuleInjection?)Activator.CreateInstance(item);
                iinjection?.RegisterServices(services);
            }
        }
    }
}