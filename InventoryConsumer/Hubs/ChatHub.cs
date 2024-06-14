using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace NotificationConsumer.Hubs
{
	public class ChatHub : Hub
	{
		private readonly ILogger<ChatHub> _logger;

		public ChatHub(ILogger<ChatHub> logger)

		{
			_logger = logger;
			
			// Log the connection
			//_logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
		}

		public async Task SendMessage(string user, string message)
		{
			// Log the received message
			_logger.LogInformation("Received message from {User}: {Message}", user, message);

			// Send the message to all clients
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
