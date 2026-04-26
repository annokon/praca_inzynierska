using backend.Notification.DTOs;

namespace backend.Notification.Services;

public interface INotificationService
{
    Task<List<NotificationDTO>> GetUserNotificationsAsync(int userId, int limit);
    Task<int> GetUnreadCountAsync(int userId);
    Task MarkAsReadAsync(int userId, int notificationId);
    Task MarkAllAsReadAsync(int userId);
    Task CreateAsync(Notification notification);
}