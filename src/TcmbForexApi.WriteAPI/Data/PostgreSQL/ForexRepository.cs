using TcmbForexApi.WriteAPI.Core.Entities;
using TcmbForexApi.WriteAPI.Core.Repositories;

namespace TcmbForexApi.WriteAPI.Data.PostgreSQL
{
    public class ForexRepository(IConfiguration configuration, DatabaseInitializer databaseInitializer) : BaseRepository<ForexRate>(configuration), IForexRepository
    {
        public async Task<int> AddCurrencyAsync(ForexRate currency)
        {
            var tableExists = await CheckTableExists();

            if (!tableExists)
            {
                await databaseInitializer.InitializeAsync();
            }

            string query = "INSERT INTO forex_rates (rate_date, code, unit, name, forex_buying, forex_selling, banknote_buying, banknote_selling, cross_rate_usd) VALUES (@Date, @Code, @Unit, @Name, @ForexBuying, @ForexSelling, @BanknoteBuying, @BanknoteSelling, @CrossRateUSD) ON CONFLICT (rate_date, code) DO NOTHING;";

            object parameters = new
            {
                currency.Date,
                currency.Code,
                currency.Unit,
                currency.Name,
                currency.ForexBuying,
                currency.ForexSelling,
                currency.BanknoteBuying,
                currency.BanknoteSelling,
                currency.CrossRateUSD
            };
            return await ExecuteAsync(query, parameters);
        }

        async Task<bool> CheckTableExists()
        {
            string query = "SELECT CASE WHEN EXISTS ( SELECT 1 FROM information_schema.tables WHERE table_schema = 'public' AND table_name = 'forex_rates' ) THEN 1 ELSE 0 END;";

            var result = await ExecuteAsync(query);

            return result == 1; 
        }
    }
}