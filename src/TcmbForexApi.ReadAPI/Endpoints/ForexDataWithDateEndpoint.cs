using FastEndpoints;
using TcmbForexApi.ReadAPI.Business.Abstracts;
using TcmbForexApi.ReadAPI.Domain.Requests;
using TcmbForexApi.ReadAPI.Domain.Responses;

namespace TcmbForexApi.ReadAPI.Endpoints
{
    public class ForexDataWithDateEndpoint(IForexRateService forexRateService) : Endpoint<ForexDataWithDateRequest, BaseResponse>
    {
        public override void Configure()
        {
            Get("/read/forex-by-date");
            AllowAnonymous();
        }

        public override async Task<BaseResponse> ExecuteAsync(ForexDataWithDateRequest req, CancellationToken ct)
        {
            var result = await forexRateService.GetForexRatesByDate(req.Date);

            return result;
        }
    }
}