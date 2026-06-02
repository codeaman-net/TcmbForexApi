using TcmbForexApi.WriteAPI.Core.Entities;

namespace TcmbForexApi.WriteAPI.Core.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        int Execute(string command, object? parameters = null);
        Task<int> ExecuteAsync(string command, object? parameters = null);
        T? QueryFirstOrDefault(string query, object? parameters = null);
        Task<T?> QueryFirstOrDefaultAsync(string query, object? parameters = null);
        IEnumerable<T>? Query(string query, object? parameters = null);       
        Task<IEnumerable<T>?> QueryAsync(string query, object? parameters = null);
    }
}