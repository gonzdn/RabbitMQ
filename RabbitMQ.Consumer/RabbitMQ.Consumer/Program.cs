using RabbitMQ.Client;
using System;
using parameters = RabbitMQ.Consumer.Utils.Constants.Parameters;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri($"amqp://guest:guest@{parameters.RabbitMQURL}")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //- Uncomment the ExchangeType you want to test, don't forget to do the same on the producer

            //QueueConsumer.Consume(channel);
            DirectExchangeConsumer.Consume(channel);
            //TopicExchangeConsumer.Consume(channel);
            //HeaderExchangeConsumer.Consume(channel);
            //FanoutExchangeConsumer.Consume(channel);
        }
    }
}