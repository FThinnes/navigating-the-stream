namespace KafkaCommon;

public class SensorEvent
{
    public long Timestamp { get; set; }
    public long SensorId { get; set; }
    public double NewSensorValue { get; set; }
}