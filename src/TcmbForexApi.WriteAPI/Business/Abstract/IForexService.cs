using TcmbForexApi.WriteAPI.Core;
using TcmbForexApi.WriteAPI.Business.DTOs;

namespace TcmbForexApi.WriteAPI.Business.Abstract
{
    public interface IForexService
    {
        Task<BaseResponse> AddForexAsync(ForexRateInsertDto insertDto);
    }
}