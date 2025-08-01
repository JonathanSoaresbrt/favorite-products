using Favorite.Products.Application.Constants;
using Favorite.Products.Application.Interfaces.Repositoryes;
using Favorite.Products.Application.Interfaces.Services;
using Favorite.Products.Domain.Models.Entities;

namespace Favorite.Products.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Customer>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Customer?> GetByIdAsync(long id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Customer?> GetByEmailAsync(string email) =>
            await _repository.GetByEmailAsync(email);

        public async Task AddAsync(Customer customer)
        {
            bool isReactived = await ValidateInactive(customer);

            if (isReactived)
                return;

            await _repository.AddAsync(customer);

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _repository.Update(customer);
            await _repository.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer != null)
            {
                customer.Inactivate();
                await UpdateAsync(customer);
            }
        }

        private async Task<bool> ValidateInactive(Customer customer)
        {
            bool isReactiveted = false;

            var customerExists = await _repository.GetByEmailAsync(customer.Email.Value);

            if (customerExists is not null && !customerExists.Inactive)
                throw new InvalidOperationException(MessagesConstants.CustomerEmailAlready);

            if (customerExists is not null && customerExists.Inactive)
            {
                customerExists.Activate();
                await UpdateAsync(customerExists);
                isReactiveted = true;
            }

            return isReactiveted;
        }
    }
}
