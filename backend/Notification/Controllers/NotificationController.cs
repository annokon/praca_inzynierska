using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using backend.Notification.Services;

namespace backend.Notification.Controllers;

[ApiController]
[Route("api/notifications")]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _service;

    public NotificationController(INotificationService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int limit = 20)
    {
        var userId = GetUserId();

        var result = await _service.GetUserNotificationsAsync(userId, limit);
        return Ok(result);
    }
    
    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var userId = GetUserId();

        var count = await _service.GetUnreadCountAsync(userId);
        return Ok(count);
    }
    
    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var userId = GetUserId();

        await _service.MarkAsReadAsync(userId, id);
        return NoContent();
    }
    
    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var userId = GetUserId();

        await _service.MarkAllAsReadAsync(userId);
        return NoContent();
    }
}