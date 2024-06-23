using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands;

public record AddCustomer(string FullName, DateTime BirthDate, string Email) : IRequest<Guid>
{
    
}