using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using queueParameters = RabbitMQ.Producer.Utils.Constants.QueueParameters;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare(queueParameters.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "",
                    routingKey: queueParameters.QueueName,
                    null,
                    body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}