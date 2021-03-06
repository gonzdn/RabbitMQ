# RabbitMQ
Basic RabbitMQ project that explains the use of a normal Queue and also the differences of using exchanges.

# Technologies
- C#
- .NET 5.0
- Docker
- RabbitMQ 

# A little bit about RabbitMQ
In this demo I'm using 4 examples of how to use Exchanges and 1 example using a single Queue.

For the exchange examples:

- Direct Exchange : Needs to have a route key defined so it can produce and consume only on that specific exchange.
- Topic Exchange  : Sames a direct, but it can have a wilder on the route key name, so it can capture everything on the same route key pattern name.
- Header Exchange : This doesn't requires a router key, instead it uses a list of properties, so you can specifie as many as you want.
- Fanout Exchange : Takes everything from the exchange no matter what router key or properties it has.

# Installation
First you need to install docker desktop 
[Docker Desktop](https://docs.docker.com/desktop/windows/install/)

After installing Docker, you need to download and install RabbitMQ, you can do this by executing the following command:
```
  docker run --rm -it --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

Execute the "Producer" project and go to your RabbitMQ Managment page: http://localhost:15672
You'll see that the queue has been created, then execute the "Consumer" project.
this will start to generate messages so the consumer can receive it, look at RabbitMQ to see the graph.
You can change the exchange type on both consumer and producer program class to see how they interact.
DirectExchangePublisher is set by default

```
  QueueProducer.Publish(channel);
  DirectExchangePublisher.Publish(channel);
  TopicExchangePublisher.Publish(channel);
  HeaderExchangePublisher.Publish(channel);
  FanoutExchangePublisher.Publish(channel);
```

# How this works
The producer is going to generate infinite messages and the consumer is going to receive the message from a queue that's listening to everything.
This way we can understand how RabbitMQ works using the queues.


