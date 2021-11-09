using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using fanoutExchangeParameters = RabbitMQ.Consumer.Utils.Constants.FanoutExchangeParameters;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(fanoutExchangeParameters.ExchangeName, ExchangeType.Fanout);
            channel.QueueDeclare(fanoutExchangeParameters.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            channel.QueueBind(fanoutExchangeParameters.QueueName,
                fanoutExchangeParameters.ExchangeName,
                string.Empty);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(fanoutExchangeParameters.QueueName,
                autoAck: true,
                consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}