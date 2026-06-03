namespace TcmbForexApi.WriteAPI.Core.Entities
{
    public class ForexRate : IEntity
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
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