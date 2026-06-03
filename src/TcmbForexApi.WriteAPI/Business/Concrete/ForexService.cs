using TcmbForexApi.WriteAPI.Business.Abstract;
using TcmbForexApi.WriteAPI.Core;
using TcmbForexApi.WriteAPI.Business.DTOs;
using TcmbForexApi.WriteAPI.Core.Entities;
using TcmbForexApi.WriteAPI.Core.Handlers;
using TcmbForexApi.WriteAPI.Core.Repositories;

namespace TcmbForexApi.WriteAPI.Business.Concrete
{
    public class ForexService(IForexRepository forexRepository) : ServiceHandler, IForexService
    {
        public async Task<BaseResponse> AddForexAsync(ForexRateInsertDto insertDto)
        {
            return await ExecuteAsync(async () =>
            {
                if (string.IsNullOrEmpty(insertDto.Code) || insertDto.ForexBuying <= 0 || insertDto.ForexSelling <= 0)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Invalid currency code or rate."
                    };
                }

                var currency = new ForexRate
                {
                    Code = insertDto.Code,
                    Name = insertDto.Name,
                    Unit = insertDto.Unit,
                    ForexBuying = insertDto.ForexBuying,
                    ForexSelling = insertDto.ForexSelling,
                    BanknoteBuying = insertDto.BanknoteBuying,
                    BanknoteSelling = insertDto.BanknoteSelling,
                    CrossRateUSD = insertDto.CrossRateUSD
                };

                var result = await forexRepository.AddCurrencyAsync(currency);
                
                return result > 0 ? "Currency added successfully." : "Failed to add currency.";
            });
        }
    }
}