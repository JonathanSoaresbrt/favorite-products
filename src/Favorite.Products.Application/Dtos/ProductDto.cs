namespace Favorite.Products.Application.Dtos
{
    public record struct ProductDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string? Review { get; set; }

        public ProductDto()
        {
            Id = 0;
            Title = string.Empty;
            Price = 0;
            Description = string.Empty;
            Category = string.Empty;
            Image = string.Empty;
            Review = string.Empty;
        }
    }
}