// Infrastructure/Kafka/TripEventProducer.cs
using Confluent.Kafka;
using RouteService.App.Interface;
using RouteService.Domain.Entities;
using System.Text.Json;

public class TripEventProducer : ITripEventPublisher
{
    private readonly IProducer<string, string> _producer;
    private readonly string _topic = "trip-events";

    public TripEventProducer(IConfiguration config)
    {
        var kafkaConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<string, string>(kafkaConfig).Build();
    }

    public async Task PublishAsync(string eventType, Trip trip)
    {
        var message = new
        {
            id = trip.Id,
            action = eventType,
            payload = trip
        };

        var json = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(_topic, new Message<string, string>
        {
            Key = trip.Id,
            Value = json
        });
    }

}
