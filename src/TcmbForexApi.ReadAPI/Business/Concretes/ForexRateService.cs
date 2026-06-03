using TcmbForexApi.ReadAPI.Business.Abstracts;
using TcmbForexApi.ReadAPI.Core.Handlers;
using TcmbForexApi.ReadAPI.Core.Repositories;
using TcmbForexApi.ReadAPI.Domain.Responses;

namespace TcmbForexApi.ReadAPI.Business.Concretes
{
    public class ForexRateService(IForexRateRepository forexRateRepository) : ServiceHandler, IForexRateService
    {
        public async Task<BaseResponse> GetForexRateByCodeAndDate(string code, DateOnly date)
        {
            return await ExecuteAsync(async () =>
            {
                var data = await forexRateRepository.GetAsync(x => x.Code == code && x.Date == date);

                if (data != null)
                {
                    return new ForexDataResponse
                    {
                        Code = data.Code,
                        Date = data.Date,
                        BanknoteBuying = data.BanknoteBuying,
                        BanknoteSelling = data.BanknoteSelling,
                        ForexBuying = data.ForexBuying,
                        ForexSelling = data.ForexSelling,
                        Name = data.Name,
                        Unit = data.Unit,
                        CrossRateUSD = data.CrossRateUSD
                    };
                }
                return data!;
            });
        }

        public async Task<BaseResponse> GetForexRatesByCode(string code)
        {
            return await ExecuteAsync(async () =>
            {
                var data = await forexRateRepository.ListAsync(x => x.Code == code);

                if (data != null)
                {
                    return data.Select(x => new ForexDataResponse
                    {
                        Code = x.Code,
                        Date = x.Date,
                        BanknoteBuying = x.BanknoteBuying,
                        BanknoteSelling = x.BanknoteSelling,
                        ForexBuying = x.ForexBuying,
                        ForexSelling = x.ForexSelling,
                        Name = x.Name,
                        Unit = x.Unit,
                        CrossRateUSD = x.CrossRateUSD
                    }).ToList();
                }
                return data!;
            });
        }

        public async Task<BaseResponse> GetForexRatesByDate(DateOnly date)
        {
            return await ExecuteAsync(async () =>
            {
                var data = await forexRateRepository.ListAsync(x => x.Date == date);

                if (data != null)
                {
                    return data.Select(x => new ForexDataResponse
                    {
                        Code = x.Code,
                        Date = x.Date,
                        BanknoteBuying = x.BanknoteBuying,
                        BanknoteSelling = x.BanknoteSelling,
                        ForexBuying = x.ForexBuying,
                        ForexSelling = x.ForexSelling,
                        Name = x.Name,
                        Unit = x.Unit,
                        CrossRateUSD = x.CrossRateUSD
                    }).ToList();
                }
                return data!;
            });
        }
    }
}