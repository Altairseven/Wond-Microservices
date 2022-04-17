using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wond.Shared.MessageBus.Models;
using IConnection = RabbitMQ.Client.IConnection;

namespace Wond.Shared.MessageBus.Client;

public interface IMessageBusClient {
    IModel? _channel { get; }

    void SendMessage<T>(string Event, T message);
}

public class MessageBusClient : IDisposable, IMessageBusClient {

    private readonly IConfiguration _conf;
    private readonly IConnection? _connection;
    private readonly ILogger<MessageBusClient> _logger;

    public IModel? _channel { get; }

    public MessageBusClient(IConfiguration conf, ILogger<MessageBusClient> logger) {
        _conf = conf;
        _logger = logger;

        ConnectionFactory factory = new ConnectionFactory {
            HostName = _conf["RabbitMQ:Host"],
            Port = int.Parse(_conf["RabbitMQ:Port"]),
        };

        try {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "general", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabitMQConnectionShutdown;

            _logger.LogInformation("--> Connected to Message Bus");

        }
        catch (Exception ex) {
            _logger.LogInformation($"--> could not connecto to the message bus: {ex.Message}");
        }
    }

    void IDisposable.Dispose() {
        _logger.LogInformation("--> Connected to Message Bus");
        if (_channel != null && _channel.IsOpen) {
            _channel.Close();
            _connection?.Dispose();

        }
    }

    private void RabitMQConnectionShutdown(object? sender, ShutdownEventArgs e) {
        _logger.LogInformation($"--> Bus Connection Shutdown:");
    }

    public void SendMessage<T>(string Event, T message) {
        var body = new MessageBusPayload<T>(Event, message);

        SendBytesThroughBus(body.ToBytes());

        _logger.LogInformation($"--> Message published to the bus ({message})");
    }

    private void SendBytesThroughBus(Byte[] bytes) {
        _channel.BasicPublish(exchange: "general", routingKey: "", basicProperties: null, body: bytes);
    }


}
