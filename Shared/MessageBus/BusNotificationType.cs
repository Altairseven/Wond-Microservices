using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wond.Shared.MessageBus;

public enum BusEventType {
    Undetermined,
    PlainText,
    AccountCreated,
    AccountLoggedIn,
    ProductAdded,
    ProductEdited,
    ProductDeleted,
    StockIngress,
    StockEgress
}
