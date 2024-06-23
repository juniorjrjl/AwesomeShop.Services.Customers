namespace AwesomeShop.Services.Customers.Infrastructure.MessageBus.IntegrationEvents;

public record CustomerCreatedIntegration(Guid Id, string FullName, string Email) : IEvent
{
    
}