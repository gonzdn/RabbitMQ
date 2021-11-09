using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using directExchangeParameters = RabbitMQ.Producer.Utils.Constants.DirectExchangeParameters;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(exchange: directExchangeParameters.ExchangeName,
                ExchangeType.Direct,
                arguments: ttl);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(directExchangeParameters.ExchangeName,
                    directExchangeParameters.ExchangeArguments,
                    null,
                    body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}