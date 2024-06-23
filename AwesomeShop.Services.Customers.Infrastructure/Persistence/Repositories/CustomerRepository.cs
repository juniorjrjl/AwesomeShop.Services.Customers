using AwesomeShop.Services.Customers.Core.Entities;
using AwesomeShop.Services.Customers.Core.Repositories;

namespace AwesomeShop.Services.Customers.Infrastructure.Persistence.Repositories;

public class CustomerRepository(IMongoRepository<Customer> mongoRepository) : ICustomerRepository
{

    private readonly IMongoRepository<Customer> _mongoRepository = mongoRepository;

    public async Task AddAsync(Customer customer) => await _mongoRepository.AddAsync(customer);

    public async Task<Customer> GetByIdAsync(Guid id) => await _mongoRepository.GetAsync(id);

    public async Task UpdateAsync(Customer customer) => await _mongoRepository.UpdateAsync(customer);

}