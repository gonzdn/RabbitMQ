using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using queueParameters = RabbitMQ.Consumer.Utils.Constants.QueueParameters;

namespace RabbitMQ.Consumer
{
    public static class QueueConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.QueueDeclare($"{queueParameters.QueueName}",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume($"{queueParameters.QueueName}",
                autoAck: true,
                consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}