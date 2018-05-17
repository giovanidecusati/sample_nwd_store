using BuildingBlock.Core.DI;
using System;

namespace BuildingBlock.Core.DomainEvents.Events
{
    public static class DomainEvents
    {
        public static IContainer Container { get; set; }

        public static void Raise<T>(T args) where T : DomainEvent
        {
            try
            {
                if (Container != null)
                {
                    var subscribers = Container.GetServices(typeof(IDomainEventHandler<T>));
                    foreach (var subscriber in subscribers)
                        ((IDomainEventHandler<T>)subscriber).Handle(args);
                }
            }
            catch (Exception ex)
            {
                throw new RaiseEventException("Failed to raise a domain event.", ex);
            }
        }
    }
}
