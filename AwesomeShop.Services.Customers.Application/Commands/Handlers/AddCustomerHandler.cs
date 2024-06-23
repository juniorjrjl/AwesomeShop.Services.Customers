using AwesomeShop.Services.Customers.Application.Mappers;
using AwesomeShop.Services.Customers.Core.Entities;
using AwesomeShop.Services.Customers.Core.Repositories;
using AwesomeShop.Services.Customers.Infrastructure.MessageBus;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands.Handlers;

public class AddCustomerHandler(ICustomerRepository repository, IEventProcessor eventProcessor, ICustomerMapper mapper) : IRequestHandler<AddCustomer, Guid>
{
    
    private readonly ICustomerRepository _repository = repository;
    private readonly IEventProcessor _eventProcessor = eventProcessor;
    private readonly ICustomerMapper _mapper = mapper;

    public async Task<Guid> Handle(AddCustomer request, CancellationToken cancellationToken)
    {
        var customer = _mapper.ToCustomer(request);

        await _repository.AddAsync(customer);

        _eventProcessor.Process(customer.Events);

        return customer.Id;
    }

}