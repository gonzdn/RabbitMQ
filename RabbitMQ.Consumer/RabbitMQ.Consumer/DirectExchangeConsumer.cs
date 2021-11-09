using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using directExchangeParameters = RabbitMQ.Consumer.Utils.Constants.DirectExchangeParameters;

namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(directExchangeParameters.ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(directExchangeParameters.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind(directExchangeParameters.QueueName,
                directExchangeParameters.ExchangeName,
                directExchangeParameters.ExchangeArguments);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(directExchangeParameters.QueueName, autoAck: true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}