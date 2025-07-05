// Infrastructure/Kafka/TripEventConsumer.cs
using Confluent.Kafka;
using FuelService.App;
using FuelService.App.Events;
using FuelService.Domain.Entities;
using System.Text.Json;

namespace FuelService.Infrastructure;
public class TripEventConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _config;

    public TripEventConsumer(IServiceScopeFactory scopeFactory, IConfiguration config)
    {
        _scopeFactory = scopeFactory;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = "trip-event-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe("trip-events");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var cr = consumer.Consume(stoppingToken);
                var message = JsonSerializer.Deserialize<TripAuditEvent>(cr.Message.Value);

                using var scope = _scopeFactory.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<TripEventHandler>();

                await handler.HandleAsync(message);
            }
            catch (Exception ex)
            {
                // log error
            }
        }
    }
}

