using Npgsql;

namespace TcmbForexApi.WriteAPI.Data.PostgreSQL
{
    public class DatabaseInitializer(IConfiguration configuration)
    {
        public async Task InitializeAsync()
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");

            await using var connection = new NpgsqlConnection(connectionString);

            await connection.OpenAsync();

            var scriptPath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "SqlScripts",
                "CreateForexRates.sql");

            var sql = await File.ReadAllTextAsync(scriptPath);

            await using var command = new NpgsqlCommand(sql, connection);

            await command.ExecuteNonQueryAsync();
        }
    }
}