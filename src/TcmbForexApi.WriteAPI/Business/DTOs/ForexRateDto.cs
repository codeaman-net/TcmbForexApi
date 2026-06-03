namespace TcmbForexApi.WriteAPI.Business.DTOs
{
    public class ForexRateDto
    {
        public string Code { get; set; } = string.Empty;
        public int Unit { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal ForexBuying { get; set; }
        public decimal ForexSelling { get; set; }
        public decimal BanknoteBuying { get; set; }
        public decimal BanknoteSelling { get; set; }
        public decimal? CrossRateUSD { get; set; }
    }
}