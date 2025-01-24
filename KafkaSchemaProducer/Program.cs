using Chr.Avro.Confluent;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using KafkaCommon;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9094",
    SaslMechanism = SaslMechanism.Plain,
    SecurityProtocol = SecurityProtocol.SaslPlaintext,
    SaslUsername = "kafka",
    SaslPassword = "mysecretpassword",
};

var registryConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081",
};
var registry = new CachedSchemaRegistryClient(registryConfig);

var producer = new ProducerBuilder<long, SensorEvent>(config)
    .SetAvroValueSerializer(registry, AutomaticRegistrationBehavior.Always).Build();
while (true)
{
    var evt = new SensorEvent
    {
        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        SensorId = Random.Shared.NextInt64(1, 5),
        NewSensorValue = Random.Shared.NextDouble() * 100
    };
    var result = producer.ProduceAsync("sensorValues", new Message<long, SensorEvent>
    {
        Key = evt.SensorId,
        Value = evt
    }).ContinueWith(task => Console.WriteLine($"Message sent: {task.Result.Status}"));
    Thread.Sleep(1000);
}