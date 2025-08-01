using Favorite.Products.Infra.Constants;
using Microsoft.Extensions.Configuration;

namespace Favorite.Products.Infra.Data.ConnectionSettings
{
    public class ConnectionStringContext
    {
        private static readonly Lazy<ConnectionStringContext> _instance = new(() => new ConnectionStringContext());
        public static ConnectionStringContext Instance => _instance.Value;

        private IConfiguration? _configuration;

        private ConnectionStringContext() { }

        public void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string name)
        {
            if (_configuration == null)
                throw new InvalidOperationException(InfraConstants.ExceptionConnectionStringContext);

            return _configuration.GetConnectionString(name) ?? throw new InvalidOperationException(InfraConstants.ExceptionConnectionNotFound);
        }
    }
}
