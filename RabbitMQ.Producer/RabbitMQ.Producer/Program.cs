using RabbitMQ.Client;
using System;
using parameters = RabbitMQ.Producer.Utils.Constants.Parameters;

namespace RabbitMQ.Producer
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

            //- Uncomment the ExchangeType you want to test, don't forget to do the same on the consumer            

            //QueueProducer.Publish(channel);
            DirectExchangePublisher.Publish(channel);
            //TopicExchangePublisher.Publish(channel);
            //HeaderExchangePublisher.Publish(channel);
            //FanoutExchangePublisher.Publish(channel);
        }
    }
}