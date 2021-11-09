using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using fanoutExchangeParameters = RabbitMQ.Producer.Utils.Constants.FanoutExchangeParameters;

namespace RabbitMQ.Producer
{
    static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchange: fanoutExchangeParameters.ExchangeName,
                ExchangeType.Fanout,
                arguments: ttl);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "update" } };

                channel.BasicPublish(exchange: fanoutExchangeParameters.ExchangeName,
                    routingKey: fanoutExchangeParameters.RoutingKey,
                    properties,
                    body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}