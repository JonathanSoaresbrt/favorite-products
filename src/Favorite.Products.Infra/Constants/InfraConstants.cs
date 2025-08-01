namespace Favorite.Products.Infra.Constants
{
    public static class InfraConstants
    {
        public const string AspNetCoreEnv = "ASPNETCORE_ENVIRONMENT";
        public const string EnvironmentDEV = "Development";
        public const string EnvironmentPRD = "Production";
        public const string JsonFileNameEnviriomentDev = "appsettings.Development.json";
        public const string JsonFileNameEnvirioment = "appsettings.json";
        public const string RelativePathProject = "favorite-products/src/Favorite.Products.API";
        public const string NpgTime = "Npgsql.EnableLegacyTimestampBehavior";
        public const string ConnectionString = "FavoriteProductsPostgres";
        public const string ModuleInjectionInterface = "IInfraModuleInjectionInfra";
        public const string NamePasteProject = "favorite-products";

        #region MessageExceptions
        public const string ExceptionConnectionStringContext = "Configuration has not been set.";
        public const string ExceptionConnectionNotFound = "Connection string not found.";
        public const string ConnectionStringConfigRoute = "ConnectionStrings:FavoriteProductsPostgres";
        public const string ExceptionConnectionString = "Connection string 'FavoriteProductsPostgres' not found in configuration.";
        public const string ExceptionDiretoryNotFound = "Directory not found in path.";
        public const string ExceptionFavoriteProductsDbContext = "*** Context creation error -- Unable to find connection. ***";
        
        #endregion
    }
}