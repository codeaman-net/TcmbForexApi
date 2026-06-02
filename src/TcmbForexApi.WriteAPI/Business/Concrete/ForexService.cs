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
        public async Task<BaseResponse> AddForexAsync(CurrencyInsertDto currenyInsertDto)
        {
            return await HandleAsync(async () =>
            {
                // Validate the input DTO
                if (string.IsNullOrEmpty(currenyInsertDto.Code) || currenyInsertDto.ForexBuying <= 0 || currenyInsertDto.ForexSelling <= 0)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Invalid currency code or rate."
                    };
                }

                var currency = new Currency
                {
                    Code = currenyInsertDto.Code,
                    Name = currenyInsertDto.Name,
                    Unit = currenyInsertDto.Unit,
                    ForexBuying = currenyInsertDto.ForexBuying,
                    ForexSelling = currenyInsertDto.ForexSelling,
                    BanknoteBuying = currenyInsertDto.BanknoteBuying,
                    BanknoteSelling = currenyInsertDto.BanknoteSelling,
                    CrossRateUSD = currenyInsertDto.CrossRateUSD
                };

                var result = await forexRepository.AddCurrencyAsync(currency);
                
                return new BaseResponse
                {
                    Success = result > 0,
                    Data = result > 0 ? "Currency added successfully." : "Failed to add currency."
                };

            });
        }
    }
}