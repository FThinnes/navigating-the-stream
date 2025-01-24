using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9094",
    SaslMechanism = SaslMechanism.Plain,
    SecurityProtocol = SecurityProtocol.SaslPlaintext,
    SaslUsername = "kafka",
    SaslPassword = "mysecretpassword",
    GroupId = "mygroup",
};

var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Assign(new TopicPartitionOffset("demo", 
    0, Offset.Beginning));
while (true)
{
    var r = consumer.Consume(10000);
    if (r == null)
    {
        Thread.Sleep(1000);
        continue;
    }
    Console.WriteLine($"Consumed message '{r.Message.Value}'" +
                      $" at: '{r.Topic}/{r.Partition}/{r.Offset}'.");
}
