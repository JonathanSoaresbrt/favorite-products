
using System.Text.RegularExpressions;
using Favorite.Products.Domain.Constants;

namespace Favorite.Products.Domain.ValueObjects
{
    public class Email
    {
        private static readonly Regex EmailRegex = new(EmailConst.RegexEmail,
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Address { get; private set;}
        public string Value => Address;

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(MessagesConst.MessageEmailAddressNotNull, nameof(address));

            if (!IsValid(address))
                throw new ArgumentException(MessagesConst.MessageInvalidInputEmail, nameof(address));

            Address = address.Trim().ToLowerInvariant();
        }

        public void UpdateAddress(string address)
        {
            this.Address = address;
        }
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim();
            return EmailRegex.IsMatch(email);
        }

        public override string ToString() => Address;

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new Email(value);
    }
}
