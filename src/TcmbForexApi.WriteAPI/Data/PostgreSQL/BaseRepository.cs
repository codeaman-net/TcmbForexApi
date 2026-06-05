using Dapper;
using Npgsql;
using TcmbForexApi.WriteAPI.Core.Entities;
using TcmbForexApi.WriteAPI.Core.Repositories;

namespace TcmbForexApi.WriteAPI.Data.PostgreSQL
{
    public class BaseRepository<TEntity>(IConfiguration configuration) : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly string _connectionString = configuration.GetConnectionString("PostgreSQL") ?? string.Empty;

        public int Execute(string command, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                return connection.Execute(command, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TcmbForexApi.WriteAPI.Data.PostgreSQL.BaseRepository -> Execute() throws an error. Message is {ex.Message}");
                return 0;
            }
        }

        public async Task<int> ExecuteAsync(string command, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                return await connection.ExecuteAsync(command, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TcmbForexApi.WriteAPI.Data.PostgreSQL.BaseRepository -> ExecuteAsync() throws an error. Message is {ex.Message}");
                return 0;
            }
        }

        public TEntity? QueryFirstOrDefault(string query, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                return connection.QueryFirstOrDefault<TEntity>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TcmbForexApi.WriteAPI.Data.PostgreSQL.BaseRepository -> QueryFirstOrDefault() throws an error. Message is {ex.Message}");
                return null;
            }
        }

        public async Task<TEntity?> QueryFirstOrDefaultAsync(string query, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<TEntity>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TcmbForexApi.WriteAPI.Data.PostgreSQL.BaseRepository -> QueryFirstOrDefaultAsync() throws an error. Message is {ex.Message}");
                return null;
            }
        }

        public IEnumerable<TEntity>? Query(string query, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                return connection.Query<TEntity>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TcmbForexApi.WriteAPI.Data.PostgreSQL.BaseRepository -> Query() throws an error. Message is {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>?> QueryAsync(string query, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                return await connection.QueryAsync<TEntity>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TcmbForexApi.WriteAPI.Data.PostgreSQL.BaseRepository -> QueryAsync() throws an error. Message is {ex.Message}");
                return null;
            }
        }
    }
}