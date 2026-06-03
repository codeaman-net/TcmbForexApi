
using TcmbForexApi.Consumers.Business.Dtos;

namespace TcmbForexApi.Consumers.Business.Abstracts
{
    public interface IForexRateService
    {
        Task CreateRateAsync(RateCreateDto createDto);
        Task DeleteRateAsync(RateDeleteDto deleteDto);
    }
}