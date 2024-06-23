namespace AwesomeShop.Services.Customers.Core.Events;

public record CustomerCreated(Guid Id, string FullName, string Email) : IDomainEvent
{
    
}