using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands;

public record UpdateCustomer(Guid Id, string PhoneNumber, AddressDTO Address) : IRequest
{
    
}
