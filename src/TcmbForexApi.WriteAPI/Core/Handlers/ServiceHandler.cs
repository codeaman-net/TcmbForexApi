namespace TcmbForexApi.WriteAPI.Core.Handlers
{
    public abstract class ServiceHandler
    {
        protected async Task<BaseResponse> HandleAsync(Func<Task<BaseResponse>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return new BaseResponse
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}