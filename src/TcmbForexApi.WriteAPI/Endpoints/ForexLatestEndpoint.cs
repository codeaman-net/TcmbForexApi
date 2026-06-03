using FastEndpoints;
using TcmbForexApi.WriteAPI.Business.Abstract;
using TcmbForexApi.WriteAPI.Business.DTOs;
using TcmbForexApi.WriteAPI.Core;
using TcmbForexApi.WriteAPI.Infrastructure.Abstracts;

namespace TcmbForexApi.WriteAPI.Endpoints
{
    public class ForexLatestEndpoint(IForexService forexService, ITcmbService tcmbService) : Endpoint<BaseResponse>
    {
        public override void Configure()
        {
            Post("/write/forex-latest");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BaseResponse req, CancellationToken ct)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            var rates = await tcmbService.ReadForexRates(today);

            foreach (var rate in rates)
            {
                var newRate = new ForexRateInsertDto
                {
                    Date = today,
                    Code = rate.Code,
                    Name = rate.Name,
                    Unit = rate.Unit,
                    ForexBuying = rate.ForexBuying,
                    ForexSelling = rate.ForexSelling,
                    BanknoteBuying = rate.BanknoteBuying,
                    BanknoteSelling = rate.BanknoteSelling,
                    CrossRateUSD = rate.CrossRateUSD
                };

                await forexService.AddForexAsync(newRate);
            }

            await Send.OkAsync(new BaseResponse
            {
                Success = true,
                Data = "Latest forex data inserted successfully."
            }, cancellation: ct);
        }
    }
}