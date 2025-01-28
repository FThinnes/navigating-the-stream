using Confluent.Kafka;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9094",
    SaslMechanism = SaslMechanism.Plain,
    SecurityProtocol = SecurityProtocol.SaslPlaintext,
    SaslUsername = "kafka",
    SaslPassword = "mysecretpassword",
};

var producer = new ProducerBuilder<string, string>(config).Build();
while (true)
{
    producer.Produce("demo", new Message<string, string>
    {
        Key = "mykey",
        Value = $"It is {DateTime.UtcNow:O}"
    }, report => Console.WriteLine($"Message sent: {report.Status}"));
    Thread.Sleep(1000);
}
