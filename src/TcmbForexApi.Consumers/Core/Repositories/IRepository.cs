using System.Linq.Expressions;
using TcmbForexApi.Consumers.Core.Entities;

namespace TcmbForexApi.Consumers.Core.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> expression);
        Task UpdateAsync(T entity, Expression<Func<T, bool>> expression);
    }
}