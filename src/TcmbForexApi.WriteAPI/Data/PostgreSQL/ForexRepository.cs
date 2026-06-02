using TcmbForexApi.WriteAPI.Core.Entities;
using TcmbForexApi.WriteAPI.Core.Repositories;

namespace TcmbForexApi.WriteAPI.Data.PostgreSQL
{
    public class ForexRepository(IConfiguration configuration) : BaseRepository<Currency>(configuration), IForexRepository
    {
        public async Task<int> AddCurrencyAsync(Currency currency)
        {
            string query = "INSERT INTO forex (code, unit, name, forexbuying, forexselling, banknotebuyying, banknoteselling, crossrateusd) VALUES (@Code, @Unit, @Name, @ForexBuying, @ForexSelling, @BanknoteBuying, @BanknoteSelling, @CrossRateUSD)";

            object parameters = new
            {
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