using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using topicExchangeParameters = RabbitMQ.Producer.Utils.Constants.TopicExchangeParameters;

namespace RabbitMQ.Producer
{
    static class TopicExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchange: topicExchangeParameters.ExchangeName,
                ExchangeType.Topic,
                arguments: ttl);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(topicExchangeParameters.ExchangeName,
                    topicExchangeParameters.ExchangeArguments,
                    null,
                    body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}