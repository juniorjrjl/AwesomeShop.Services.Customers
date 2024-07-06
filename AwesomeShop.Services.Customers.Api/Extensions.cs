using Consul;

namespace AwesomeShop.Services.Customers.Api;

public static class Extensions
{
    
    public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
    {
        var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("Extensions");
        var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

        var registration = new AgentServiceRegistration()
        {
            ID = $"customer-service",
            Name = "CustomerServices",
            Address = "customerapi",
            Port = 5002
        };

        logger.LogInformation("Registering with Consul");
        consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

        lifetime.ApplicationStopping.Register(() =>
        {
            logger.LogInformation("Unregistering from Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
        });

        return app;
    }

    public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            var address = configuration.GetValue<string>("Consul:Host");
            consulConfig.Address = new Uri(address);
        }));

        return services;
    }

}