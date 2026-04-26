namespace backend.Notification.DTOs;

public class NotificationDTO
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? MessageId { get; set; }
    public int? TripInvitationId { get; set; }
    public int? ReviewId { get; set; }
}