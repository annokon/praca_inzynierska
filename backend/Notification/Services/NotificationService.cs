using backend.Notification.DTOs;
using backend.Notification.Repositories;

namespace backend.Notification.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repo;

    public NotificationService(INotificationRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<NotificationDTO>> GetUserNotificationsAsync(int userId, int limit)
    {
        var notifications = await _repo.GetByUserIdAsync(userId, limit);

        return notifications.Select(n => new NotificationDTO
        {
            Id = n.IdNotification,
            Type = n.Type,
            IsRead = n.IsRead,
            CreatedAt = n.CreatedAt,
            MessageId = n.IdMessage,
            TripInvitationId = n.IdTripInvitation,
            ReviewId = n.IdReview
        }).ToList();
    }

    public async Task<int> GetUnreadCountAsync(int userId)
    {
        return await _repo.GetUnreadCountAsync(userId);
    }

    public async Task MarkAsReadAsync(int userId, int notificationId)
    {
        await _repo.MarkAsReadAsync(notificationId, userId);
        await _repo.SaveChangesAsync();
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        await _repo.MarkAllAsReadAsync(userId);
        await _repo.SaveChangesAsync();
    }

    public async Task CreateAsync(Notification notification)
    {
        await _repo.AddAsync(notification);
        await _repo.SaveChangesAsync();
    }
}