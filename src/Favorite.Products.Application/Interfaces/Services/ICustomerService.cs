using Favorite.Products.Domain.Models.Entities;

namespace Favorite.Products.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(long id);
        Task<Customer?> GetByEmailAsync(string email);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(long id);
        Task SaveChangesAsync();
    }
}