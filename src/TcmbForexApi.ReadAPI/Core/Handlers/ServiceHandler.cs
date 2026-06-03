using TcmbForexApi.ReadAPI.Domain.Responses;

namespace TcmbForexApi.ReadAPI.Core.Handlers
{
    public abstract class ServiceHandler
    {
        protected async Task<BaseResponse> ExecuteAsync(Func<Task<object>> func)
        {
            try
            {
                var data = await func();

                return new BaseResponse
                {
                    Success = true,
                    Data = data
                };
            }
            catch (Exception)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "An error occurred while processing the request. Please try again later."
                };
            }
        }
    }
}