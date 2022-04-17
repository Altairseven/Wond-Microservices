using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wond.Shared.MessageBus.Client;

public interface IMessageBusClient {
    void SendPlainText(string message);
}
