namespace RabbitMQ.Consumer.Utils
{
    public class Constants
    {
        public struct Parameters {
             public const string RabbitMQURL = "localhost:5672";
        }        
        public struct QueueParameters
        {
            public const string QueueName = "demo-queue";
        }
        public struct DirectExchangeParameters
        {
            public const string ExchangeName = "demo-direct-exchange";
            public const string QueueName = "demo-direct-queue";
            public const string ExchangeArguments = "account.init";
        }
        public struct TopicExchangeParameters
        {
            public const string ExchangeName = "demo-topic-exchange";
            public const string QueueName = "demo-topic-queue";
            public const string ExchangeArguments = "account.*";
        }
        public struct HeaderExchangeParameters
        {
            public const string ExchangeName = "demo-header-exchange";
            public const string QueueName = "demo-header-queue";
        }
        public struct FanoutExchangeParameters
        {
            public const string ExchangeName = "demo-fanout-exchange";
            public const string QueueName = "demo-fanout-queue";
        }
    }
}