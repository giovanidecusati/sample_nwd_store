using System;

namespace BuildingBlock.Core.DomainEvents.Events
{
    public abstract class DomainEvent
    {
        public DateTime Date { get; private set; }

        protected DomainEvent()
        {
            Date = DateTime.UtcNow;
        }
    }
}
