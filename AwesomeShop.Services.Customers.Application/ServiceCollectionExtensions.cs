using AwesomeShop.Services.Customers.Application.Mappers;
using AwesomeShop.Services.Customers.Application.Subscribes;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Customers.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSubscribers(this IServiceCollection services) {
        services.AddHostedService<CustomerCreatedSubscriber>();     

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerMapper, CustomerMapper>();
        return services;
    }
}