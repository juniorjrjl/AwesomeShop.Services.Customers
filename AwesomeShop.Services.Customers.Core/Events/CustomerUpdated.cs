using AwesomeShop.Services.Customers.Core.ValueObjects;

namespace AwesomeShop.Services.Customers.Core.Events;

public record CustomerUpdated(Guid Id, string PhoneNumber, Address Address) : IDomainEvent
{
    
}