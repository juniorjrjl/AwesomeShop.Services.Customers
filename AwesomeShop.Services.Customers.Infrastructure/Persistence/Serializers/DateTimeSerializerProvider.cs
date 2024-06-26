using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AwesomeShop.Services.Customers.Infrastructure.Persistence.Serializers;

public class DateTimeSerializerProvider : IBsonSerializationProvider
{
    
    public IBsonSerializer? GetSerializer(Type type) => type == typeof(DateTime) ? DateTimeSerializer.LocalInstance : null;

}
