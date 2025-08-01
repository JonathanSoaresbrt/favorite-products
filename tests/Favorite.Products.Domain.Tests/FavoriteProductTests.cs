#nullable enable
using Favorite.Products.Domain.Models.Entities;
using Xunit;

namespace Favorite.Products.Domain.Tests
{
    public class FavoriteProductTests
    {
        [Fact]
        public void Constructor_ShouldInitializeCorrectly()
        {
            var now = DateTime.UtcNow;

            var product = new FavoriteProduct(
                productId: 1,
                customerId: 10,
                title: "Test",
                urlImage: "http://image.com",
                price: 20.50m,
                review: "5",
                inactive: false,
                dateCreated: now,
                dateUpdated: now
            );

            Assert.Equal("Test", product.Title);
            Assert.Equal(20.50m, product.Price);
            Assert.False(product.Inactive);
            Assert.Equal(now, product.DateCreated, TimeSpan.FromSeconds(1));
            Assert.Equal(now, product.DateUpdated, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_ShouldThrow_WhenTitleIsInvalid(string? title)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new FavoriteProduct(1, 1, title!, "http://img", 10, "rev", false, DateTime.UtcNow, DateTime.UtcNow)
            );

            Assert.Contains("title", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_ShouldThrow_WhenUrlImageIsInvalid(string? image)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new FavoriteProduct(1, 1, "Title", image!, 10, "rev", false, DateTime.UtcNow, DateTime.UtcNow)
            );

            Assert.Contains("urlImage", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenPriceIsZero()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new FavoriteProduct(1, 1, "Title", "http://img", 0, "rev", false, DateTime.UtcNow, DateTime.UtcNow)
            );

            Assert.Contains("price", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Inactivate_ShouldMarkProductAsInactive()
        {
            var product = CreateValidProduct();
            product.Inactivate();

            Assert.True(product.Inactive);
        }

        [Fact]
        public void Activate_ShouldMarkProductAsActive()
        {
            var product = CreateValidProduct();
            product.Inactivate();
            product.Activate();

            Assert.False(product.Inactive);
        }

        private FavoriteProduct CreateValidProduct()
        {
            return new FavoriteProduct(
                productId: 1,
                customerId: 1,
                title: "Test",
                urlImage: "http://img",
                price: 10,
                review: "rev",
                inactive: false,
                dateCreated: DateTime.UtcNow,
                dateUpdated: DateTime.UtcNow
            );
        }
    }
}
