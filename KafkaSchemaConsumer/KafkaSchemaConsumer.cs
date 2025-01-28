using Chr.Avro.Confluent;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using KafkaCommon;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9094",
    SaslMechanism = SaslMechanism.Plain,
    SecurityProtocol = SecurityProtocol.SaslPlaintext,
    SaslUsername = "kafka",
    SaslPassword = "mysecretpassword",
    GroupId = "myconsumer"
};

var registryConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081",
};
var registry = new CachedSchemaRegistryClient(registryConfig);

var consumer = new ConsumerBuilder<long, SensorEvent>(config)
    .SetAvroValueDeserializer(registry).Build();
consumer.Assign(new TopicPartitionOffset("sensorValues", 0, Offset.Beginning));
while (true)
{
    var result = consumer.Consume(10000);
    if (result == null)
    {
        Thread.Sleep(1000);
        continue;
    }
    Console.WriteLine($"New sensor value: {result.Message.Value.NewSensorValue} at: {result.Message.Value.Timestamp} for sensor {result.Message.Value.SensorId}");
}
