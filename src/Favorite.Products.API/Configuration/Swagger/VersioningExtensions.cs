using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Favorite.Products.API.Configuration.Swagger;

internal static class VersioningExtensions
{
    public static IServiceCollection AddApiFavoriteProductsVersioning(this IServiceCollection services)
    {

        services.AddApiVersioning(options =>
        {
            options.UseApiBehavior = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);

            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";

            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenNewtonsoftSupport();

        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    public static IApplicationBuilder UseFavoriteProductsVersioning(this IApplicationBuilder app)
    {
        app.UseSwagger(options => { options.RouteTemplate = "favoriteproducts/swagger/{documentname}/swagger.json"; });

        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "favoriteproducts/swagger";

            var apiVersionDescriptionProvider =
                app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
        });

        return app;
    }
}