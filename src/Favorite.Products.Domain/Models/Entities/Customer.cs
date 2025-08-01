

using Favorite.Products.Domain.ValueObjects;

namespace Favorite.Products.Domain.Models.Entities
{
    public class Customer
    {
        protected Customer()
        {
            FavoriteProducts = new List<FavoriteProduct>();
        }

        public Customer(string name, Email email, DateTime dateCreated, DateTime dateUpdated)
        {
            Name = name;
            Email = email;
            Inactive = false;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            FavoriteProducts = new List<FavoriteProduct>();
        }

        public long Id { get; private set; }
        public string? Name { get; private set; }
        public Email? Email { get; private set; }
        public bool Inactive { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateCreated { get; private set; }

        public List<FavoriteProduct> FavoriteProducts { get; private set; }

        public void AddFavoriteProduct(FavoriteProduct product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            if (!FavoriteProducts.Any(p => p.Title == product.Title && p.UrlImage == product.UrlImage))
            {
                FavoriteProducts.Add(product);
                DateUpdated = DateTime.UtcNow;
            }
        }

        public void RemoveFavoriteProduct(long favoriteProductId)
        {
            var product = FavoriteProducts.FirstOrDefault(p => p.ProductId == favoriteProductId);
            if (product != null)
            {
                FavoriteProducts.Remove(product);
                DateUpdated = DateTime.UtcNow;
            }
        }

        public void Update(string name, string email)
        {
            Name = name;
            if (Email != null)
            {
                Email.UpdateAddress(email);
            }
            DateUpdated = DateTime.UtcNow;
        }

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
