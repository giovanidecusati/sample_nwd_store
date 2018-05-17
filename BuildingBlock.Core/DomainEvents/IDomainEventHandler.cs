using BuildingBlock.Core.DomainEvents.Events;
using System;
using System.Threading.Tasks;

namespace BuildingBlock.Core.DomainEvents
{
    public interface IDomainEventHandler<T> : IDisposable where T : DomainEvent
    {
        Task Handle(T args);
    }
}
