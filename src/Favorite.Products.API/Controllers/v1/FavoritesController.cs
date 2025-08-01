using Favorite.Products.API.Dtos.Request;
using Favorite.Products.Application.Constants;
using Favorite.Products.Application.Interfaces.Services;
using Favorite.Products.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Favorite.Products.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/customers/{customerId:long}/favorites")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly ILogger<FavoritesController> _logger;
        private readonly IFavoriteProductService _favoriteProductService;

        public FavoritesController(
            ILogger<FavoritesController> logger,
            IFavoriteProductService favoriteProductService)
        {
            _logger = logger;
            _favoriteProductService = favoriteProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites(long customerId)
        {
            if (customerId <= 0)
                return BadRequest(new { error = MessagesConstants.CustomerObrigatory, statusCode = 400 });

            var favorites = await _favoriteProductService.GetFavoritesByCustomerAsync(customerId);
            return Ok(favorites);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFavorite(long customerId, [FromBody] FavoriteProductRequest request)
        {
            if (customerId <= 0)
                return BadRequest(new { error = MessagesConstants.CustomerObrigatory, statusCode = 400 });

            request.Validate();

            var favorite = new FavoriteProduct(
                productId: request.ProductId,
                customerId: customerId,
                title: request.Title,
                urlImage: request.UrlImage,
                price: request.Price,
                review: request.Review,
                inactive: false,
                dateCreated: DateTime.UtcNow,
                dateUpdated: DateTime.UtcNow
            );

            await _favoriteProductService.AddAsync(favorite);

            var response = new
            {
                ProductId = favorite.ProductId,
                favorite.Title,
                favorite.UrlImage,
                favorite.Price,
                favorite.Review
            };

            return CreatedAtAction(nameof(GetFavorites), new { customerId }, response);
        }

        [HttpDelete("{productId:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveFavorite(long customerId, long productId)
        {
            if (customerId <= 0)
                return BadRequest(new { error = MessagesConstants.CustomerObrigatory, statusCode = 400 });

            if (productId <= 0)
                return BadRequest(new { error = MessagesConstants.ProductObrigatory, statusCode = 400 });

            await _favoriteProductService.RemoveAsync(customerId, productId);
            return NoContent();
        }
    }
}
