using Newtonsoft.Json;

namespace TcmbForexApi.Consumers.Core.Models
{
    public class RateMessageModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("rate_date")]
        public int RateDate { get; set; }
        
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;
        
        [JsonProperty("unit")]
        public int Unit { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonProperty("forex_buying")]
        public decimal ForexBuying { get; set; }
        
        [JsonProperty("forex_selling")]
        public decimal ForexSelling { get; set; }
        
        [JsonProperty("banknote_buying")]
        public decimal BanknoteBuying { get; set; }
        
        [JsonProperty("banknote_selling")]
        public decimal BanknoteSelling { get; set; }
        
        [JsonProperty("cross_rate_usd")]
        public decimal? CrossRateUSD { get; set; }
    }
}