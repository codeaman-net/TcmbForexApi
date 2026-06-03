using System.Globalization;
using System.Xml.Linq;
using FastEndpoints;
using TcmbForexApi.WriteAPI.Business.Abstract;
using TcmbForexApi.WriteAPI.Business.DTOs;
using TcmbForexApi.WriteAPI.Core;
using TcmbForexApi.WriteAPI.Core.Requests;
using TcmbForexApi.WriteAPI.Infrastructure.Abstracts;

namespace TcmbForexApi.WriteAPI.Endpoints
{
    public class ForexWithDateEndpoint(IForexService forexService, ITcmbService tcmbService) : Endpoint<ForexWithDateRequest, BaseResponse>
    {
        public override void Configure()
        {
            Post("/write/forex-custom");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ForexWithDateRequest req, CancellationToken ct)
        {
            if (req.Date >= DateOnly.FromDateTime(DateTime.Now))
            {
                await Send.StatusCodeAsync(404, cancellation: ct);
            }

            var rates = await tcmbService.ReadForexRates(req.Date);

            foreach (var rate in rates)
            {
                var newRate = new ForexRateInsertDto
                {
                    Date = req.Date,
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
                Data = "Forex data inserted successfully."
            }, cancellation: ct);
        }
    }
}