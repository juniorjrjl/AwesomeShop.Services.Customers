using System.Text;
using AwesomeShop.Services.Customers.Core.Events;
using AwesomeShop.Services.Customers.Infrastructure.MessageBus.IntegrationEvents;

namespace AwesomeShop.Services.Customers.Infrastructure.MessageBus;

public class EventProcessor(IMessageBusClient bus) : IEventProcessor
{
    private readonly IMessageBusClient _bus = bus;

    public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events) => events.Select(Map);

    public IEvent? Map(IDomainEvent @event) 
        => @event switch
            {
                CustomerCreated e => new CustomerCreatedIntegration(e.Id, e.FullName, e.Email),
                _ => null
            };

    public void Process(IEnumerable<IDomainEvent> events)
    {
        var integrationEvents = MapAll(events);

        foreach (var @event in integrationEvents) {
            _bus.Publish(@event, MapConvention(@event), "customer-service");
        }
    }

    private string MapConvention(IEvent @event) => ToDashCase(@event.GetType().Name);

    public string ToDashCase(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        
        if (text.Length < 2) {
            return text;
        }
        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for(int i = 1; i < text.Length; ++i) {
            char c = text[i];
            if(char.IsUpper(c)) {
                sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            } else {
                sb.Append(c);
            }
        }

        Console.WriteLine($"ToDashCase: "+ sb.ToString());

        return sb.ToString();
    }
}