
using TcmbForexApi.Consumers.Business.Dtos;

namespace TcmbForexApi.Consumers.Business.Abstracts
{
    public interface IForexRateService
    {
        Task<bool> CreateRateAsync(RateCreateDto createDto);
    }
}