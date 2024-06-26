using System.Linq.Expressions;
using AwesomeShop.Services.Customers.Core.Entities;
using MongoDB.Driver;

namespace AwesomeShop.Services.Customers.Infrastructure.Persistence.Repositories;

public class MongoRepository<T>(IMongoDatabase database, string collectionName) : IMongoRepository<T> where T : IEntityBase
{

    public IMongoCollection<T> Collection { get; private set; } = database.GetCollection<T>(collectionName);

    public async Task AddAsync(T entity) => await Collection.InsertOneAsync(entity);

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate) => await Collection.Find(predicate).ToListAsync();

    public async Task<T> GetAsync(Guid id) => (await Collection.FindAsync(e => e.Id.Equals(id))).First();

    public async Task UpdateAsync(T entity) => await Collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);

}