using AwesomeShop.Services.Customers.Core.Events;

namespace AwesomeShop.Services.Customers.Core.Entities;

public abstract class AggregationRoot : IEntityBase
{
    private List<IDomainEvent> _events = [];

    public Guid Id { get; set; }

    public IEnumerable<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @event)
    {
        _events ??= [];
        _events.Add(@event);
    }

}