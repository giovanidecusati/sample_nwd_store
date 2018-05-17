using BuildingBlock.Core.DomainEvents.Events;
using System;

namespace Nwd.BackOffice.SalesContext.Events
{
    public class ProductApprovedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public ProductApprovedEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
