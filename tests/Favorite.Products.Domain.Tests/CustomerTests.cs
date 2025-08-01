using Favorite.Products.Domain.Models.Entities;
using Favorite.Products.Domain.ValueObjects;
using Xunit;
using System;
using System.Linq;

namespace Favorite.Products.Domain.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Constructor_ShouldInitializeCorrectly()
        {
            var now = DateTime.UtcNow;
            var customer = new Customer("Test", new Email("jonathan@email.com"), now, now);

            Assert.Equal("Test", customer.Name);
            Assert.Equal("jonathan@email.com", customer.Email.Value);
            Assert.False(customer.Inactive);
            Assert.Empty(customer.FavoriteProducts);
        }

        [Fact]
        public void Update_ShouldChangeNameAndEmail()
        {
            var customer = new Customer("Old", new Email("jonathanold@email.com"), DateTime.UtcNow, DateTime.UtcNow);
            var newEmail = new Email("jonathannew@email.com");

            customer.Update("New Name", newEmail);

            Assert.Equal("New Name", customer.Name);
            Assert.Equal("jonathannew@email.com", customer.Email.Value);
        }

        [Fact]
        public void Inactivate_ShouldSetInactiveToTrue()
        {
            var customer = new Customer("Test", new Email("jonathan@email.com"), DateTime.UtcNow, DateTime.UtcNow);

            customer.Inactivate();

            Assert.True(customer.Inactive);
        }

        [Fact]
        public void Activate_ShouldSetInactiveToFalse()
        {
            var customer = new Customer("Test", new Email("jonathan@email.com"), DateTime.UtcNow, DateTime.UtcNow);
            customer.Inactivate();

            customer.Activate();

            Assert.False(customer.Inactive);
        }

        [Fact]
        public void AddFavoriteProduct_ShouldAddWhenNotExists()
        {
            var customer = new Customer("Test", new Email("jonathan@email.com"), DateTime.UtcNow, DateTime.UtcNow);
            var product = CreateFakeProduct();

            customer.AddFavoriteProduct(product);

            Assert.Single(customer.FavoriteProducts);
            Assert.Equal(product.ProductId, customer.FavoriteProducts.First().ProductId);
        }

        [Fact]
        public void AddFavoriteProduct_ShouldNotAddDuplicate()
        {
            var customer = new Customer("Test", new Email("jonathan@email.com"), DateTime.UtcNow, DateTime.UtcNow);
            var product = CreateFakeProduct();

            customer.AddFavoriteProduct(product);
            customer.AddFavoriteProduct(product);

            Assert.Single(customer.FavoriteProducts);
        }

        [Fact]
        public void RemoveFavoriteProduct_ShouldRemoveIfExists()
        {
            var customer = new Customer("Test", new Email("jonathan@email.com"), DateTime.UtcNow, DateTime.UtcNow);
            var product = CreateFakeProduct();

            customer.AddFavoriteProduct(product);
            customer.RemoveFavoriteProduct(product.ProductId);

            Assert.Empty(customer.FavoriteProducts);
        }

        private FavoriteProduct CreateFakeProduct()
        {
            return new FavoriteProduct(
                productId: 1,
                customerId: 1,
                title: "Test Product",
                urlImage: "http://image.com",
                price: 10,
                review: "4.5",
                inactive: false,
                dateCreated: DateTime.UtcNow,
                dateUpdated: DateTime.UtcNow
            );
        }
    }
}
