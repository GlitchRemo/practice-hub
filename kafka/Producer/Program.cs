// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;

var config = new ProducerConfig()
{
    BootstrapServers = "localhost:9092",
    ClientId = "riya-producer"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    var result = await producer.ProduceAsync("riya-topic", new Message<Null, string>()
    {
        Value = "Ami je tomar"
    });

    Console.WriteLine($"Message produces to {result.TopicPartitionOffset}");
}
catch (ProduceException<Null, string> e)
{
    Console.WriteLine($"Error while producing message: {e.Message}, {e.Error.Code}");
}
