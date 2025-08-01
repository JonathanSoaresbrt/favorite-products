using Favorite.Products.API.Configuration.Swagger;
using Favorite.Products.Application.Constants;
using Favorite.Products.Application.Dtos.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Favorite.Products.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<TokenJwtConfiguration>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddApiFavoriteProductsVersioning();
            return services;
        }

        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient(ApiExternalConstants.FakeStoreApi, client =>
            {
                client.BaseAddress = new Uri(ApiExternalConstants.UrlFakeStore);
                client.DefaultRequestHeaders.Add(ApiExternalConstants.HeaderAccept, ApiExternalConstants.HeaderJson);
            });

            return services;
        }
    }
}
