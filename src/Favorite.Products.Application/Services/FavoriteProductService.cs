using Favorite.Products.Application.Constants;
using Favorite.Products.Application.Dtos;
using Favorite.Products.Application.Interfaces.Repositoryes;
using Favorite.Products.Application.Interfaces.Services;
using Favorite.Products.Domain.Models.Entities;

namespace Favorite.Products.Application.Services
{
    public class FavoriteProductService : IFavoriteProductService
    {
        private readonly IFavoriteProductRepository _repositoryProductRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IExternalProductService _externalProductService;
        public FavoriteProductService(IFavoriteProductRepository repository, IExternalProductService externalProductService, ICustomerRepository customerRepository)
        {
            _repositoryProductRepository = repository;
            _externalProductService = externalProductService;
            _customerRepository = customerRepository;
        }

        public async Task<List<ProductDto>> GetFavoritesByCustomerAsync(long customerId)
        {
            var products = await _repositoryProductRepository.GetByCustomerIdAsync(customerId);

            return products.Select(p => new ProductDto
            {
                Id = p.ProductId,
                Title = p.Title,
                Image = p.UrlImage,
                Price = p.Price,
                Review = p.Review
            }).ToList();
        }

        public async Task AddAsync(FavoriteProduct favoriteProduct)
        {
            var customerExists = await _customerRepository.GetByIdAsync(favoriteProduct.CustomerId);
            if (customerExists == null)
                throw new InvalidOperationException(MessagesConstants.CustomerNotFoundOrInactive);

            await ValidateExternalProductExistsAsync(favoriteProduct.ProductId);

            bool isReactived = await ValidateInactive(favoriteProduct);

            if (isReactived)
                return;

            await _repositoryProductRepository.AddAsync(favoriteProduct);

            await _repositoryProductRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(long customerId, long productId)
        {
            var favorite = await _repositoryProductRepository.GetByCustomerAndProductIdAsync(customerId, productId);

            if (favorite is null)
                throw new InvalidOperationException(MessagesConstants.FavoriteProductNotFound);

            favorite.Inactivate();

            _repositoryProductRepository.Update(favorite);

            await _repositoryProductRepository.SaveChangesAsync();
        }

        private async Task ValidateExternalProductExistsAsync(long productId)
        {
            var product = await _externalProductService.GetProductByIdAsync(productId);

            if (product is null)
                throw new InvalidOperationException(MessagesConstants.ProductNotFoundApiExt);
        }

        private async Task<bool> ValidateInactive(FavoriteProduct favoriteProduct)
        {
            bool isReactiveted = false;

            var favoriteExists = await _repositoryProductRepository.GetByCustomerAndProductIdAsync(favoriteProduct.CustomerId, favoriteProduct.ProductId);

            if (favoriteExists is not null && !favoriteExists.Inactive)
                throw new InvalidOperationException(MessagesConstants.ProducAlready);

            if (favoriteExists is not null && favoriteExists.Inactive)
            {
                favoriteExists.Activate();
                _repositoryProductRepository.Update(favoriteExists);
                await _repositoryProductRepository.SaveChangesAsync();
                isReactiveted = true;
            }

            return isReactiveted;
        }
    }
}
