using Favorite.Products.Infra.Constants;
using Microsoft.Extensions.Configuration;

namespace Favorite.Products.Infra.Data.ConnectionSettings
{
    public static class ConnectionStringGenerateMigration
    {
        public static string GetConnectionStringMigration()
        {
            string relativePath = GetDynamicPathBeforeTarget(Directory.GetCurrentDirectory(), InfraConstants.NamePasteProject);

            var config = new ConfigurationBuilder()
                                                .SetBasePath($"{relativePath}{InfraConstants.RelativePathProject}")
                                                .AddJsonFile(InfraConstants.JsonFileNameEnvirioment, false)
                                                .Build();

            var connectionString = config[InfraConstants.ConnectionStringConfigRoute];

            return connectionString ?? throw new InvalidOperationException(InfraConstants.ExceptionConnectionString);
        }

        private static string GetDynamicPathBeforeTarget(string fullPath, string targetDirectory)
        {
            int targetIndex = fullPath.IndexOf(targetDirectory, StringComparison.Ordinal);

            if (targetIndex == -1)
            {
                throw new ArgumentException(InfraConstants.ExceptionDiretoryNotFound);
            }

            return fullPath.Substring(0, targetIndex);
        }
    }
}