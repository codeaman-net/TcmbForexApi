using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TcmbForexApi.ReadAPI.Core.Entities;
using TcmbForexApi.ReadAPI.Core.Repositories;

namespace TcmbForexApi.ReadAPI.Data.MongoDB
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IMongoCollection<TEntity> _collection;
        public BaseRepository(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            _collection = database.GetCollection<TEntity>(options.Value.CollectionName);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression) => await _collection.Find(expression).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression) => await _collection.Find(expression).ToListAsync();
    }
}