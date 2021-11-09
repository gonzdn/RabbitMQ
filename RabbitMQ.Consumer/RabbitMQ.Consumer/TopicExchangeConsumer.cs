using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using topicExchangeParameters = RabbitMQ.Consumer.Utils.Constants.TopicExchangeParameters;

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(topicExchangeParameters.ExchangeName, ExchangeType.Topic);
            channel.QueueDeclare(topicExchangeParameters.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind(topicExchangeParameters.QueueName,
                topicExchangeParameters.ExchangeName,
                topicExchangeParameters.ExchangeArguments);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(topicExchangeParameters.QueueName, true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}