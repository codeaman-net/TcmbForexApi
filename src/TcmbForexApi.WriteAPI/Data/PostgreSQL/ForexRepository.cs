using TcmbForexApi.WriteAPI.Core.Entities;
using TcmbForexApi.WriteAPI.Core.Repositories;

namespace TcmbForexApi.WriteAPI.Data.PostgreSQL
{
    public class ForexRepository(IConfiguration configuration) : BaseRepository<ForexRate>(configuration), IForexRepository
    {
        public async Task<int> AddCurrencyAsync(ForexRate currency)
        {
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
    }
}