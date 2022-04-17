using System.Text.Json;
using Wond.Shared.Dtos;
using Wond.Shared.MessageBus.Models;
using Wond.Shared.MessageBus.Subscriber;

namespace Wond.Notifications.BusEvents;

public enum EventType {
    texto, test, Undetermined
}


public class NotificationsEventProcesor: IEventProcessor {

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<NotificationsEventProcesor> _logger;
    //private IMapper _mapper;

    public NotificationsEventProcesor(IServiceScopeFactory scopeFactory, ILogger<NotificationsEventProcesor> logger
        //,IMapper mapper
        ) {

        _scopeFactory = scopeFactory;
        _logger = logger;
        //_mapper = mapper;
    }

    public void ProcessEvent(string message) {
        var eventType = DetermineEvent(message);

        switch (eventType) {
            case EventType.texto:
                var payloadt = JsonSerializer.Deserialize<MessageBusPayload<string>>(message);
                _logger.LogInformation(payloadt!.payload);

                break;
            case EventType.test:
                var payloadc = JsonSerializer.Deserialize<MessageBusPayload<ProductColor>>(message);
                _logger.LogInformation(payloadc!.payload!.Nombre);

                break;
            case EventType.Undetermined:
                break;
            default:
                break;
        }
    }


    private EventType DetermineEvent(string notificationMessage) {
        var eventType = JsonSerializer.Deserialize<MessageBusPayload>(notificationMessage);

        EventType result;
        switch (eventType!.Event) {
            case "texto":
                _logger.LogInformation("Texto simple Event Detected");
                result = EventType.texto;
                break;
            case "test":
                _logger.LogInformation("ProductoColor Event Detected");
                result = EventType.test;
                break;
            default:
                _logger.LogInformation("Could not determinine event Type");
                result = EventType.Undetermined;
                break;
        }
        return result;
    }
}
