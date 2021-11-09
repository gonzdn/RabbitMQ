using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using headerExchangeParameters = RabbitMQ.Consumer.Utils.Constants.HeaderExchangeParameters;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(headerExchangeParameters.ExchangeName, ExchangeType.Headers);
            channel.QueueDeclare(headerExchangeParameters.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var header = new Dictionary<string, object> { { "account", "new" } };

            channel.QueueBind(headerExchangeParameters.QueueName,
                headerExchangeParameters.ExchangeName,
                string.Empty,
                header);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(headerExchangeParameters.QueueName,
                autoAck: true,
                consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}