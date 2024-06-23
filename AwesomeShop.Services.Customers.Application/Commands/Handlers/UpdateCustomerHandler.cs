using AwesomeShop.Services.Customers.Application.Mappers;
using AwesomeShop.Services.Customers.Core.Repositories;
using AwesomeShop.Services.Customers.Core.ValueObjects;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands.Handlers;

public class UpdateCustomerHandler(ICustomerRepository customerRepository, ICustomerMapper mapper) : IRequestHandler<UpdateCustomer>
{
    
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ICustomerMapper _mapper = mapper;

    public async Task Handle(UpdateCustomer request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);
        customer = _mapper.ToCustomer(customer, request);
        await _customerRepository.UpdateAsync(customer);
    }

}