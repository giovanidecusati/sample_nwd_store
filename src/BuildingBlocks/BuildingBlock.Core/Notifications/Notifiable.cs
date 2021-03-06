﻿using System.Collections.Generic;

namespace BuildingBlock.Core.Notifications
{
    public abstract class Notifiable : INotifiable
    {
        readonly List<Notification> _notifications;

        protected Notifiable() { _notifications = new List<Notification>(); }

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string property, string message)
        {
            _notifications.Add(new Notification(property, message));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(IList<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ICollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public bool HasNotifications() => _notifications.Count > 0;
    }
}
