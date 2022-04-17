using System.Text.Json;
using Wond.Shared.Dtos;
using Wond.Shared.MessageBus;

namespace Wond.Notifications.Subscribers;

public class EventProcessor : IEventProcessor {
    private readonly IServiceScopeFactory _scopeFactory;
    public readonly ILogger<EventProcessor> _logger;


    public EventProcessor(IServiceScopeFactory scopeFactory, ILogger<EventProcessor> logger) {
        _logger = logger;
        _scopeFactory = scopeFactory;
       
    }

    public void ProcessEvent(string message) {
        var eventType = DetermineEvent(message);

        switch (eventType) {
            case BusEventType.PlainText:
                _logger.LogInformation("Plain Text Recieved Through BUS:" + message);
                break;
            case BusEventType.Undetermined:
                break;
            default:
                break;
        }
    }

    private BusEventType DetermineEvent(string notificationMessage) {
        Console.WriteLine("--> Determining Eventy Type");

        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        BusEventType result;
        switch (eventType!.Event) {
            case "Platform_Published":
                Console.WriteLine("--> Platform Publish Event Detected");
                result = BusEventType.PlainText;
                break;
            default:
                Console.WriteLine("--> Could not determinine event Type");
                result = BusEventType.Undetermined;
                break;
        }
        return result;
    }

}
