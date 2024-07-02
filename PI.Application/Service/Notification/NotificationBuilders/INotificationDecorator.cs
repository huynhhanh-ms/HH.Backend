using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Application.Service.Notification.NotificationBuilders
{
    public interface INotificationDecorator<T> : INotificationBuilder 
        where T : class
    {
        INotificationDecorator<T> AddObject(T obj);
    }
}
