using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Events
{
    public class LowProductInventoryEvent : DomainEvent
    {
        public int InventoryAmountLeft { get; private set; }

        public LowProductInventoryEvent(Guid aggregateId, int inventoryAmountLeft) : base(aggregateId)
        {
            InventoryAmountLeft = inventoryAmountLeft;
        }
    }
}
