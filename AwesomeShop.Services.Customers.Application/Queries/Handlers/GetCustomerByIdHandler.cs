using AwesomeShop.Services.Customers.Application.Commands;
using AwesomeShop.Services.Customers.Application.Mappers;
using AwesomeShop.Services.Customers.Application.ViewModels;
using AwesomeShop.Services.Customers.Core.Repositories;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Queries.Handlers;

public class GetCustomerByIdHandler(ICustomerRepository repository, ICustomerMapper mapper) : IRequestHandler<GetCustomerById, GetCustomerByIdViewModel>
{

    private readonly ICustomerRepository _repository = repository;
    private readonly ICustomerMapper _mapper = mapper;

    public async Task<GetCustomerByIdViewModel> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id);

        return _mapper.ToViewModel(customer);
    }
}