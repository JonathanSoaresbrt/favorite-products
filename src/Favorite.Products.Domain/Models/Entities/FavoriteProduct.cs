using Favorite.Products.Domain.Constants;

namespace Favorite.Products.Domain.Models.Entities
{
    public class FavoriteProduct
    {
        protected FavoriteProduct()
        {
            Title = string.Empty;
            UrlImage = string.Empty;
        }

        public FavoriteProduct(long productId, long customerId, string title, string urlImage, decimal price, string? review, bool inactive, DateTime dateCreated, DateTime dateUpdated)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(MessagesConst.MessageFavoriteProductTitleEmpty, nameof(title));

            if (string.IsNullOrWhiteSpace(urlImage))
                throw new ArgumentException(MessagesConst.MessageFavoriteProductUrlEmpty, nameof(urlImage));

            if (price <= 0)
                throw new ArgumentException(MessagesConst.MessageFavoriteProductPriceEmpty, nameof(price));

            ProductId = productId;
            CustomerId = customerId;
            Title = title;
            UrlImage = urlImage;
            Price = price;
            Review = review;
            Inactive = inactive;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public long Id { get; private set; }

        public long ProductId { get; private set; }

        public long CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;

        public string Title { get; private set; }
        public string UrlImage { get; private set; }
        public decimal Price { get; private set; }
        public string? Review { get; private set; }
        public bool Inactive { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateCreated { get; private set; }

        public void Activate()
        {
            Inactive = false;
            DateUpdated = DateTime.UtcNow;
        }

        public void Inactivate()
        {
            Inactive = true;
            DateUpdated = DateTime.UtcNow;
        }
    }
}
