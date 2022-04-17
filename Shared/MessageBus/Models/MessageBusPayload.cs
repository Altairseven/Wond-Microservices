using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wond.Shared.MessageBus.Models;


public class MessageBusPayload {
    public string? Event { get; set; }

    //TODO: Agregar quizas desde assembly quien lo manda?

    public MessageBusPayload(string? Event) {
        this.Event = Event;
       
    }

   
}

public class MessageBusPayload<T> {
    public string? Event { get; set; }
    public T? payload { get; set; }

    //TODO: Agregar quizas desde assembly quien lo manda?


    public MessageBusPayload(string? Event, T? payload) { 
        this.Event = Event;
        this.payload = payload; 
    }

    public Byte[] ToBytes() {
        var jsonobj = JsonSerializer.Serialize(this);
        var bytes = Encoding.UTF8.GetBytes(jsonobj);
        return bytes;
    }
}
