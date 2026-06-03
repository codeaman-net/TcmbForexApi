
using Newtonsoft.Json;
using TcmbForexApi.Consumers.Core.Enums;

namespace TcmbForexApi.Consumers.Core.Converters
{
    public class DebeziumOperationTypeConverter : JsonConverter<DebeziumOperationType>
    {
        public override DebeziumOperationType ReadJson(
        JsonReader reader,
        Type objectType,
        DebeziumOperationType existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();

            return value switch
            {
                "r" => DebeziumOperationType.Read,
                "c" => DebeziumOperationType.Create,
                "u" => DebeziumOperationType.Update,
                "d" => DebeziumOperationType.Delete,
                _ => DebeziumOperationType.Empty
            };
        }

        public override void WriteJson(
            JsonWriter writer,
            DebeziumOperationType value,
            JsonSerializer serializer)
        {
            var jsonValue = value switch
            {
                DebeziumOperationType.Create => "c",
                DebeziumOperationType.Update => "u",
                DebeziumOperationType.Delete => "d",
                DebeziumOperationType.Read => "r",
                _ => string.Empty
            };

            writer.WriteValue(jsonValue);
        }

        public static readonly DebeziumOperationTypeConverter Singleton = new();
    }
}