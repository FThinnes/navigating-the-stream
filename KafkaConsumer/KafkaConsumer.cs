using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9094",
    SaslMechanism = SaslMechanism.Plain,
    SecurityProtocol = SecurityProtocol.SaslPlaintext,
    SaslUsername = "kafka",
    SaslPassword = "mysecretpassword",
    GroupId = "mygroup",
    EnableAutoCommit = false
};

var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Assign(new TopicPartitionOffset("demo", 
    0, Offset.Stored));
while (true)
{
    var r = consumer.Consume(); // will block until a message is available

    Console.WriteLine($"Consumed message with offset {r.Offset} - value {r.Message.Value}");
}
