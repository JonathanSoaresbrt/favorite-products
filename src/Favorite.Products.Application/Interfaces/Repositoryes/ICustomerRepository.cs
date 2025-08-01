using Favorite.Products.Domain.Models.Entities;

namespace Favorite.Products.Application.Interfaces.Repositoryes
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(long id);
        Task<Customer?> GetByEmailAsync(string email);
        Task<List<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        void Remove(Customer customer);
        Task SaveChangesAsync();
    }
}