using Favorite.Products.Infra.Constants;
using Microsoft.Extensions.Configuration;

namespace Favorite.Products.Infra.Data.ConnectionSettings
{
    public static class DatabaseConfiguration
    {
        public static IConfiguration GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable(InfraConstants.AspNetCoreEnv) ?? InfraConstants.EnvironmentDEV;

            var settingsFile = environment switch
            {
                InfraConstants.EnvironmentDEV => InfraConstants.JsonFileNameEnviriomentDev,
                InfraConstants.EnvironmentPRD => InfraConstants.JsonFileNameEnvirioment,
                _ => InfraConstants.JsonFileNameEnvirioment
            };

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingsFile, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
