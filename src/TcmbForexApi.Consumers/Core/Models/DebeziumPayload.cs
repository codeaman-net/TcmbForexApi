using Newtonsoft.Json;
using TcmbForexApi.Consumers.Core.Converters;
using TcmbForexApi.Consumers.Core.Enums;

namespace TcmbForexApi.Consumers.Core.Models
{
    public class DebeziumPayload<T>
    {
        [JsonProperty("before")]
        public T? Before { get; set; }

        [JsonProperty("after")]
        public T? After { get; set; }

        [JsonConverter(typeof(DebeziumOperationTypeConverter))]
        [JsonProperty("op")]
        public DebeziumOperationType Op { get; set; }
    }
}