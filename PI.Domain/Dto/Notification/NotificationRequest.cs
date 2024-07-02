namespace PI.Domain.Dto.Notification
{
    public class NotificationRequest
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        //type
        public string Type { get; set; } = null!;
        public string? Data { get; set; }
        public string? Payload { get; set; }
    }
}