using TcmbForexApi.WriteAPI.Core.Entities;

namespace TcmbForexApi.WriteAPI.Core.Repositories
{
    public interface IForexRepository
    {
        public Task<int> AddCurrencyAsync(Currency currency);
    }
}