using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Notification.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly DataContext _context;

    public NotificationRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Notification>> GetByUserIdAsync(int userId, int limit)
    {
        return await _context.Notifications
            .Where(n => n.IdUser == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<int> GetUnreadCountAsync(int userId)
    {
        return await _context.Notifications
            .CountAsync(n => n.IdUser == userId && !n.IsRead);
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
    }

    public async Task MarkAsReadAsync(int notificationId, int userId)
    {
        var notif = await _context.Notifications
            .FirstOrDefaultAsync(n => n.IdNotification == notificationId && n.IdUser == userId);

        if (notif != null)
        {
            notif.IsRead = true;
        }
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var notifs = await _context.Notifications
            .Where(n => n.IdUser == userId && !n.IsRead)
            .ToListAsync();

        foreach (var n in notifs)
            n.IsRead = true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}