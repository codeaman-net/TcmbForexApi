namespace TcmbForexApi.WriteAPI.Core.Handlers
{
    public abstract class ServiceHandler
    {
        protected async Task<BaseResponse> ExecuteAsync(Func<Task<object>> action)
        {
            try
            {
                var data = await action();
                
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