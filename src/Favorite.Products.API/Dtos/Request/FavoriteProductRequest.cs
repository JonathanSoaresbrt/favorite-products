using Favorite.Products.Application.Constants;

namespace Favorite.Products.API.Dtos.Request
{
    public class FavoriteProductRequest
    {
        public long ProductId { get; set; }
        public string Title { get; set; } = null!;
        public string UrlImage { get; set; } = null!;
        public decimal Price { get; set; }
        public string Review { get; set; }

        public void Validate()
        {
            var errors = new List<string>();

            if (ProductId <= 0)
                errors.Add(MessagesConstants.ProductObrigatory);

            if (errors.Any())
                throw new ArgumentException(string.Join("; ", errors));
        }
    }
}