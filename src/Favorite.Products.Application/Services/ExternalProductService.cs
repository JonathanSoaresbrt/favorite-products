using System.Text.Json;
using Favorite.Products.Application.Constants;
using Favorite.Products.Application.Dtos;
using Favorite.Products.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Favorite.Products.Application.Services
{
    public class ExternalProductService : IExternalProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IExternalProductService> _logger;

        public ExternalProductService(IHttpClientFactory httpClientFactory, ILogger<IExternalProductService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ProductDto?> GetProductByIdAsync(long id)
        {
            var client = _httpClientFactory.CreateClient(ApiExternalConstants.FakeStoreApi);

            var url = $"{ApiExternalConstants.RouteBaseHttp}/{ApiExternalConstants.RouteProductId.Replace("{id}", id.ToString())}";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var contentStream = await response.Content.ReadAsStreamAsync();
            if (contentStream == null || contentStream.Length == 0)
                return null;

            try
            {
                return await JsonSerializer.DeserializeAsync<ProductDto>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, ApiExternalConstants.ExceptionApi, id);
                return null;
            }
        }
    }
}
