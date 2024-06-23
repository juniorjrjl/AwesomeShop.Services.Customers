using AwesomeShop.Services.Customers.Application.Queries.Handlers;
using AwesomeShop.Services.Customers.Application.ViewModels;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Queries;

public record GetCustomerById(Guid Id) : IRequest<GetCustomerByIdViewModel>
{
    
}