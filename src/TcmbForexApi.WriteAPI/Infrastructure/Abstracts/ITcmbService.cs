using TcmbForexApi.WriteAPI.Business.DTOs;

namespace TcmbForexApi.WriteAPI.Infrastructure.Abstracts
{
    public interface ITcmbService
    {
        Task<IEnumerable<ForexRateDto>> ReadForexRates(DateOnly date);
    }
}