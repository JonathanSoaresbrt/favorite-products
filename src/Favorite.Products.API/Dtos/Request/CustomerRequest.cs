using Favorite.Products.Application.Constants;

public class CustomerRequest
{
    public string Name { get; set; }
    public string Email { get; set; }

    public void Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add(MessagesConstants.CustomerNameObrigatory);

        if (string.IsNullOrWhiteSpace(Email))
            errors.Add(MessagesConstants.CustomerEmailObrigatory);
        else if (!IsValidEmail(Email))
            errors.Add(MessagesConstants.CustomerEmailInvalid);

        if (errors.Any())
            throw new ArgumentException(string.Join("; ", errors));
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
