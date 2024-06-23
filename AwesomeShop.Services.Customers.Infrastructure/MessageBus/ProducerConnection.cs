using RabbitMQ.Client;

namespace AwesomeShop.Services.Customers.Infrastructure.MessageBus;

public class ProducerConnection(IConnection connection)
{
    public IConnection Connection { get; private set; } = connection;
    
}