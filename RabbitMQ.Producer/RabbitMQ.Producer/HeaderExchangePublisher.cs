using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using headerExchangeParameters = RabbitMQ.Producer.Utils.Constants.HeaderExchangeParameters;

namespace RabbitMQ.Producer
{
    static class HeaderExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchange: headerExchangeParameters.ExchangeName,
            ExchangeType.Headers,
            arguments: ttl);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "update" } };

                channel.BasicPublish(exchange: headerExchangeParameters.ExchangeName,
                    string.Empty,
                    properties,
                    body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}