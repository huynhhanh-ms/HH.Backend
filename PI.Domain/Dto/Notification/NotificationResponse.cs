namespace PI.Domain.Dto.Notification
{
    public class NotificationResponse
    {
        public int NotificationId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Data { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public int ReceiverId { get; set; }
    }
}