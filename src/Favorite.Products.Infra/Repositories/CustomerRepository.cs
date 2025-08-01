using Favorite.Products.Application.Interfaces.Repositoryes;
using Favorite.Products.Domain.Models.Entities;
using Favorite.Products.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Favorite.Products.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FavoriteProductsDbContext _context;

        public CustomerRepository(FavoriteProductsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.Where(c => c.Inactive == false).AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer?> GetByIdAsync(long id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id && c.Inactive == false);
        }

        public void Remove(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
