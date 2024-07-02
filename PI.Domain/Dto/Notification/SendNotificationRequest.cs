using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.Notification
{
    public class SendNotificationRequest
    {
        public NotificationRequest Notification { get; set; } = null!;
        public int[] ReceiverIds { get; set; } = null!;
    }
}
