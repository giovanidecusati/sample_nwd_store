using System.Collections.Generic;

namespace BuildingBlock.Core.Notifications
{
    public interface INotifiable
    {
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}