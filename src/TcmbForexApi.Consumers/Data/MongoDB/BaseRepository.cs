using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TcmbForexApi.Consumers.Core.Entities;
using TcmbForexApi.Consumers.Core.Repositories;

namespace TcmbForexApi.Consumers.Data.MongoDB
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IMongoCollection<TEntity> _collection;
        public BaseRepository(MongoDbSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);
            _collection = database.GetCollection<TEntity>(dbSettings.CollectionName);
        }

        public async Task CreateAsync(TEntity entity) => await _collection.InsertOneAsync(entity);

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression) => await _collection.DeleteOneAsync(expression);

        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression) => await _collection.ReplaceOneAsync(expression, entity);
    }
}