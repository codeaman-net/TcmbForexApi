using System.Linq.Expressions;
using TcmbForexApi.ReadAPI.Core.Entities;

namespace TcmbForexApi.ReadAPI.Core.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
    }
}