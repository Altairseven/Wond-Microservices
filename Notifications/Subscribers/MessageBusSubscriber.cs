
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Wond.Shared.MessageBus;

namespace Wond.Notifications.Subscribers;

public class MessageBusSubscriber : BackgroundService {

    private readonly IConfiguration _conf;
    private readonly IEventProcessor _eventProcesor;

    private IConnection? _connection;
    private IModel? _channel;
    private string? _queueName;

    public MessageBusSubscriber(IConfiguration conf, IEventProcessor eventProcesor) {
        _conf = conf;
        _eventProcesor = eventProcesor;

        InitializeRabbitMQ();
    }

    public void InitializeRabbitMQ() {
        var factory = new ConnectionFactory() {
            HostName = _conf["RabbitMQ:Host"],
            Port = int.Parse(_conf["RabbitMQ:Port"]),
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");

        Console.WriteLine("--> Listining for events in the Message bus");
        _connection.ConnectionShutdown += RabbitMQConnectionshudown;
    }

    private void RabbitMQConnectionshudown(object? sender, ShutdownEventArgs e) {
        Console.WriteLine("--> Connection To the Message BUS has shutdown");
    }

    public override void Dispose() {
        if (_channel != null && _channel.IsOpen) {
            _channel.Close();
            if (_connection != null)
                _connection.Close();
        }
        base.Dispose();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ModuleHandle, ea) => {
            Console.WriteLine("--> Event Recieved!");

            var body = ea.Body;
            var notifMessage = Encoding.UTF8.GetString(body.ToArray());

            _eventProcesor.ProcessEvent(notifMessage);
        };

        _channel.BasicConsume(_queueName, autoAck: true, consumer);

        return Task.CompletedTask;
    }
}
