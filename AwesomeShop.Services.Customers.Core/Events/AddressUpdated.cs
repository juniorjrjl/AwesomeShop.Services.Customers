namespace AwesomeShop.Services.Customers.Core.Events;

public record AddressUpdated(Guid CustomerID, string FullAddress): IDomainEvent
{
    
}
