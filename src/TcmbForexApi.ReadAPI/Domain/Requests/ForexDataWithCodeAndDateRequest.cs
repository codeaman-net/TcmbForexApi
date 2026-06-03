namespace TcmbForexApi.ReadAPI.Domain.Requests
{
    public class ForexDataWithCodeAndDateRequest
    {
        public string Code { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
    }
}