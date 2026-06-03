namespace TcmbForexApi.Consumers.Core.Configuration
{
    public class KafkaSettings
    {
        public string Host { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string TopicName { get; set; } = string.Empty;
    }
}