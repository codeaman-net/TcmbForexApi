using TcmbForexApi.ReadAPI.Domain.Responses;

namespace TcmbForexApi.ReadAPI.Business.Abstracts
{
    public interface IForexRateService
    {
        Task<BaseResponse> GetForexRatesByCode(string code);
        Task<BaseResponse> GetForexRatesByDate(DateOnly date);
        Task<BaseResponse> GetForexRateByCodeAndDate(string code, DateOnly date);
    }
}