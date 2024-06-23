namespace AwesomeShop.Services.Customers.Infrastructure.MessageBus;

public record RabbitMQOptions(string User, string Password, string Host, string VirtualHost,int Port);