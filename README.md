# Notification Service with Kafka, WebSocket (SignalR), and ASP.NET Core

Welcome to the Notification Service project! This project demonstrates a notification system using ASP.NET Core, Kafka, and WebSocket (SignalR). The system is designed to produce notifications via Kafka and broadcast them to connected clients using WebSocket.

## How our results look like 


## Table of Contents

- [Architecture](#architecture)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [Configuration](#configuration)
- [Usage](#usage)


## Architecture

The architecture of this notification service is as follows:

1. **Kafka Producer Module**: Sends notifications to Kafka topics.
2. **Kafka Consumer Module**: Listens to Kafka topics and consumes notifications.
3. **WebSocket (SignalR)**: Broadcasts consumed notifications to connected clients in real-time.

<img src="https://i.imgur.com/DUIlfh9.png" alt="Architecture Diagram" width="700"/> 

## Features

- **Kafka Integration**: Efficient message queuing and delivery.
- **SignalR**: Real-time communication with WebSocket.
- **Scalable Architecture**: Easily extendable for additional services or clients.
- **ASP.NET Core**: Modern web framework for building robust applications.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/6.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))
- [Kafka](https://kafka.apache.org/quickstart) (setup a Kafka broker)
- [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr) (SignalR)

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/your-username/notification-service.git
   cd notification-service
   ```

2. Install dependencies:

   ```sh
   dotnet restore
   ```

### Running the Application

1. Update the `appsettings.json` with your Kafka configuration:

   ```json
   {
     "Kafka": {
       "BootstrapServers": "your-kafka-bootstrap-server"
     }
   }
   ```

2. Run the application:

   ```sh
   dotnet run
   ```

### Key Files

- **Program.cs**: Configures and runs the ASP.NET Core application.
- **ProducerService.cs**: Implements Kafka producer logic.
- **ConsumerService.cs**: Implements Kafka consumer logic and uses SignalR for broadcasting messages.
- **ChatHub.cs**: Defines the SignalR hub for WebSocket communication.
- **NotificationUpdateRequest.cs**: Model for notification messages.

## Configuration

Ensure your `appsettings.json` contains the necessary Kafka configuration:

```json
{
  "Kafka": {
    "BootstrapServers": "localhost:9092"
  }
}
```

## Usage

### Sending Notifications

To send a notification, make a POST request to the `NotificationController` with the following JSON payload:

```json
{
  "id": 1,
  "deviceId": "device123",
  "warningLevel": "High",
  "notificationMessage": "Temperature exceeded threshold"
}
```

### Receiving Notifications

Connect to the SignalR hub at `/chatHub` to receive real-time notifications. Use the `ReceiveMessage` event to handle incoming notifications.

## Contributing

Contributions are welcome! Please fork this repository and submit pull requests.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature-branch`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature-branch`)
5. Create a new Pull Request

