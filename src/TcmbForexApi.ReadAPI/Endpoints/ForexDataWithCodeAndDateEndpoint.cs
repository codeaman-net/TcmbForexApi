using FastEndpoints;
using TcmbForexApi.ReadAPI.Business.Abstracts;
using TcmbForexApi.ReadAPI.Domain.Requests;
using TcmbForexApi.ReadAPI.Domain.Responses;

namespace TcmbForexApi.ReadAPI.Endpoints
{
    public class ForexDataWithCodeAndDateEndpoint(IForexRateService forexRateService) : Endpoint<ForexDataWithCodeAndDateRequest, BaseResponse>
    {
        public override void Configure()
        {
            Get("/read/forex-by-code-and-date");
            AllowAnonymous();
        }

        public override async Task<BaseResponse> ExecuteAsync(ForexDataWithCodeAndDateRequest req, CancellationToken ct)
        {
            var result = await forexRateService.GetForexRateByCodeAndDate(req.Code, req.Date);

            return result;
        }
    }
}