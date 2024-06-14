using Confluent.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR; 
using NotificationConsumer.Hubs; 
namespace NotificationConsumer.Services
{
	public class ConsumerService : BackgroundService
	{
		private readonly IConsumer<Ignore, string> _consumer;
		private readonly ILogger<ConsumerService> _logger;
		private readonly IHubContext<ChatHub> _hubContext; // Add this field

		public ConsumerService(IConfiguration configuration, ILogger<ConsumerService> logger, IHubContext<ChatHub> hubContext) // Inject IHubContext<ChatHub>
		{
			_logger = logger;
			_hubContext = hubContext; // Store the injected IHubContext<ChatHub>
			var consumerConfig = new ConsumerConfig
			{
				BootstrapServers = configuration["Kafka:BootstrapServers"],
				GroupId = "NotificationConsumerGroup",
				AutoOffsetReset = AutoOffsetReset.Earliest
			};
			_consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_consumer.Subscribe(new List<string> { "NotificationUpdates", "NotificationUpdates1" });
			_logger.LogInformation("Consumer service started.");

			while (!stoppingToken.IsCancellationRequested)
			{
				await ProcessKafkaMessageAsync(stoppingToken);
				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
			}

			_consumer.Close();
		}

		private async Task ProcessKafkaMessageAsync(CancellationToken stoppingToken)
		{
			try
			{
				var consumeTask = Task.Run(() => _consumer.Consume(stoppingToken), stoppingToken);
				var consumeResult = await consumeTask;
				var topic = consumeResult.Topic;
				var message = consumeResult.Message.Value;

				_logger.LogInformation($"Received Notification update: {message}, From Topic {topic}");

				// Use the _hubContext to send a message to all connected clients
				await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", $"Notification update: {message}");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error processing Kafka message: {ex.Message}");
			}
		}
	}
}
