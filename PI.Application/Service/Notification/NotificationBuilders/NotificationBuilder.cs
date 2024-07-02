using PI.Domain.Dto.Notification;

namespace PI.Application.Service.Notification.NotificationBuilders
{
    public class NotificationBuilder : INotificationBuilder
    {
        private SendNotificationRequest _sendNotificationReq;
        private HashSet<int> _receiverIds;

        public NotificationBuilder()
        {
            _sendNotificationReq = new SendNotificationRequest()
            {
                Notification = new NotificationRequest()
            };

            _receiverIds = new HashSet<int>();
        }

        public INotificationBuilder AddBody(string? body = null)
        {
            if (!string.IsNullOrEmpty(body))
            {
                _sendNotificationReq.Notification.Body = body;
            }
            return this;
        }

        public INotificationBuilder AddDate(string? data = null)
        {
            if (!string.IsNullOrEmpty(data))
            {
                _sendNotificationReq.Notification.Data = data;
            }
            return this;
        }

        public INotificationBuilder AddPayload(string? payload = null)
        {
            if (!string.IsNullOrEmpty(payload))
            {
                _sendNotificationReq.Notification.Payload = payload;
            }
            return this;
        }

        public INotificationBuilder AddReceivers(params int[] receiverIds)
        {
            if (receiverIds.Length > 0)
            {
                foreach (var receiverId in receiverIds)
                {
                    if(!_receiverIds.Contains(receiverId))
                    {
                        _receiverIds.Add(receiverId);
                    }
                }
            }

            return this;
        }

        public INotificationBuilder AddTitle(string? title = null)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _sendNotificationReq.Notification.Title = title;
            }
            return this;
        }

        public INotificationBuilder AddType(string? type = null)
        {
            if (!string.IsNullOrEmpty(type))
            {
                _sendNotificationReq.Notification.Type = type;
            }
            return this;
        }

        public SendNotificationRequest Build()
        {
            this._sendNotificationReq.ReceiverIds = _receiverIds.ToArray();
            return this._sendNotificationReq;
        }
    }

}

