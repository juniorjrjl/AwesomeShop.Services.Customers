using AwesomeShop.Services.Customers.Core.Entities;
using AwesomeShop.Services.Customers.Core.Repositories;
using AwesomeShop.Services.Customers.Infrastructure.MessageBus;
using AwesomeShop.Services.Customers.Infrastructure.Persistence;
using AwesomeShop.Services.Customers.Infrastructure.Persistence.Repositories;
using AwesomeShop.Services.Customers.Infrastructure.Persistence.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Customers.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services) {
            BsonSerializer.RegisterSerializationProvider(new GuidSerializerProvider());
            BsonSerializer.RegisterSerializationProvider(new DateTimeSerializerProvider());
            
            #pragma warning disable CS0618
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
            #pragma warning restore CS0618

        services.AddSingleton(s => {
            var configuration = s.GetService<IConfiguration>();
            ArgumentNullException.ThrowIfNull(configuration);

            var mongoConfig = configuration.GetSection("Mongo").Get<MongoDBOptions>();
            ArgumentNullException.ThrowIfNull(mongoConfig);

            return mongoConfig;
        });

        services.AddSingleton<IMongoClient>(sp => {
            var options = sp.GetService<MongoDBOptions>();
            ArgumentNullException.ThrowIfNull(options);

            return new MongoClient(options.ConnectionStrings);
        });

        services.AddTransient(sp => {
            var options = sp.GetService<MongoDBOptions>();
            ArgumentNullException.ThrowIfNull(options);
            var mongoClient = sp.GetService<IMongoClient>();
            ArgumentNullException.ThrowIfNull(mongoClient);

            return mongoClient.GetDatabase(options.Database);
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddMongoRepository<Customer>("customers");
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services) {
        services.AddSingleton(s => {
            var configuration = s.GetService<IConfiguration>();
            ArgumentNullException.ThrowIfNull(configuration);

            var rabbitMQConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>();
            ArgumentNullException.ThrowIfNull(rabbitMQConfig);

            return rabbitMQConfig;
        });
        services.AddSingleton(s => {
            var options = s.GetService<RabbitMQOptions>();
            ArgumentNullException.ThrowIfNull(options);

            var connectionFactory = new ConnectionFactory {
                HostName = options.Host,
                UserName = options.User,
                Password = options.Password,
                Port = options.Port,
                VirtualHost = options.VirtualHost
            };

            var connection = connectionFactory.CreateConnection("customers-service-producer"); 

            return new ProducerConnection(connection);
        });  
        services.AddSingleton<IMessageBusClient, RabbitMqClient>();
        services.AddTransient<IEventProcessor, EventProcessor>();
        
        return services;    
    }

    private static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collection) where T: IEntityBase {
        services.AddScoped<IMongoRepository<T>>(f => 
        {
            var mongoDatabase = f.GetRequiredService<IMongoDatabase>();
            ArgumentNullException.ThrowIfNull(mongoDatabase);

            return new MongoRepository<T>(mongoDatabase, collection);
        });

        return services;
    }
}