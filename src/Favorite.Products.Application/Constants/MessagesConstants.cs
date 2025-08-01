namespace Favorite.Products.Application.Constants
{
    public static class MessagesConstants
    {
        public const string ProducAlready = "This product is already in the customer's favorites list.";
        public const string FavoriteProductNotFound = "Favorite product not found.";
        public const string ProductNotFoundApiExt = "Product not found in external API.";
        public const string ProductObrigatory = "Product ID must be greater than zero.";
        public const string CustomerEmailAlready = "Email already exists.";
        public const string CustomerObrigatory = "Customer ID must be greater than zero.";
        public const string CustomerNameObrigatory = "Name is required.";
        public const string CustomerEmailObrigatory = "Email is required.";
        public const string CustomerEmailInvalid = "Invalid email format.";
        public const string MiddlewareExceptionUnhandled = "Unhandled exception";
        public const string MiddlewareException = "An unexpected error occurred.";
        public const string MiddlewareJson = "application/json";
        public const string CustomerNotFoundOrInactive = "Customer does not exist or is inactive.";
    }
}