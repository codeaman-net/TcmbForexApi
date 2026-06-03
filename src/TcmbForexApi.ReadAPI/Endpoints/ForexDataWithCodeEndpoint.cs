using FastEndpoints;
using TcmbForexApi.ReadAPI.Business.Abstracts;
using TcmbForexApi.ReadAPI.Domain.Requests;
using TcmbForexApi.ReadAPI.Domain.Responses;

namespace TcmbForexApi.ReadAPI.Endpoints
{
    public class ForexDataWithCodeEndpoint(IForexRateService forexRateService) : Endpoint<ForexDataWithCodeRequest, BaseResponse>
    {
        public override void Configure()
        {
            Get("/read/forex-by-code");
            AllowAnonymous();
        }

        public override async Task<BaseResponse> ExecuteAsync(ForexDataWithCodeRequest req, CancellationToken ct)
        {
            var result = await forexRateService.GetForexRatesByCode(req.Code);

            return result;
        }
    }
}