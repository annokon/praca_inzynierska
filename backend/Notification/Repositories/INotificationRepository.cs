namespace backend.Notification.Repositories;

public interface INotificationRepository
{
    Task<List<Notification>> GetByUserIdAsync(int userId, int limit);
    Task<int> GetUnreadCountAsync(int userId);
    Task AddAsync(Notification notification);
    Task MarkAsReadAsync(int notificationId, int userId);
    Task MarkAllAsReadAsync(int userId);
    Task SaveChangesAsync();
}