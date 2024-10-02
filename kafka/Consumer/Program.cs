// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;

Console.WriteLine("Hello, World!");

var config = new ConsumerConfig()
{
    BootstrapServers = "localhost:9092",
    GroupId = "riya-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(config).Build();

consumer.Subscribe("riya-topic");
var cts = new CancellationTokenSource();

while (true)
{
    var message = consumer.Consume(cts.Token);
    Console.WriteLine($"Consumed message '{message.Message.Value}' at: '{message.TopicPartitionOffset}'.");
}