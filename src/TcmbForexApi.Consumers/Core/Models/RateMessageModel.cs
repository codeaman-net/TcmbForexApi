using Newtonsoft.Json;

namespace TcmbForexApi.Consumers.Core.Models
{
    public class RateMessageModel
    {
        [JsonProperty(nameof(Id))]
        public int Id { get; set; }

        [JsonProperty(nameof(RateDate))]
        public DateOnly RateDate { get; set; }
        
        [JsonProperty(nameof(Code))]
        public string Code { get; set; } = string.Empty;
        
        [JsonProperty(nameof(Unit))]
        public int Unit { get; set; }
        
        [JsonProperty(nameof(Name))]
        public string Name { get; set; } = string.Empty;
        
        [JsonProperty(nameof(ForexBuying))]
        public decimal ForexBuying { get; set; }
        
        [JsonProperty(nameof(ForexSelling))]
        public decimal ForexSelling { get; set; }
        
        [JsonProperty(nameof(BanknoteBuying))]
        public decimal BanknoteBuying { get; set; }
        
        [JsonProperty(nameof(BanknoteSelling))]
        public decimal BanknoteSelling { get; set; }
        
        [JsonProperty(nameof(CrossRateUSD))]
        public decimal? CrossRateUSD { get; set; }
    }
}