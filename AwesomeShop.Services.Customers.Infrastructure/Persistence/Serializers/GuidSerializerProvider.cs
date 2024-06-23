using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AwesomeShop.Services.Customers.Infrastructure.Persistence.Serializers;

public class GuidSerializerProvider : IBsonSerializationProvider
{

    public IBsonSerializer? GetSerializer(Type type) => type == typeof(Guid) ? new GuidSerializer(GuidRepresentation.Standard) : null;

}