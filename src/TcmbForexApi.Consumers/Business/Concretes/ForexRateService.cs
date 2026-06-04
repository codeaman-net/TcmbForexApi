
using TcmbForexApi.Consumers.Business.Abstracts;
using TcmbForexApi.Consumers.Business.Dtos;
using TcmbForexApi.Consumers.Core.Entities;
using TcmbForexApi.Consumers.Core.Repositories;

namespace TcmbForexApi.Consumers.Business.Concretes
{
    public class ForexRateService(IForexRateRepository forexRateRepository) : IForexRateService
    {
        public async Task<bool> CreateRateAsync(RateCreateDto createDto)
        {
            try
            {
                var rateEntity = new Rate
                {
                    BanknoteBuying = createDto.BanknoteBuying,
                    BanknoteSelling = createDto.BanknoteSelling,
                    Code = createDto.Code,
                    CrossRateUSD = createDto.CrossRateUSD,
                    Date = createDto.Date,
                    ForexBuying = createDto.ForexBuying,
                    ForexSelling = createDto.ForexSelling,
                    Name = createDto.Name,
                    RateId = createDto.RateId,
                    Unit = createDto.Unit
                };
    
                await forexRateRepository.CreateAsync(rateEntity);

                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
    }
}