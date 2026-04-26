using System.Security.Claims;
using backend.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.User.Controllers;

[Route("api/blocked-users")]
[ApiController]
[Authorize]
public class BlockedUsersController : ControllerBase
{
    private readonly IBlockedUserService _service;

    public BlockedUsersController(IBlockedUserService service)
    {
        _service = service;
    }

    private int GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(claim, out var userId))
            throw new UnauthorizedAccessException();

        return userId;
    }
    
    [HttpPost("{id}")]
    public async Task<IActionResult> Block(int id)
    {
        var userId = GetUserId();

        var result = await _service.BlockAsync(userId, id);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "User blocked" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Unblock(int id)
    {
        var userId = GetUserId();

        var result = await _service.UnblockAsync(userId, id);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "User unblocked" });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBlockedUsers()
    {
        var userId = GetUserId();

        var users = await _service.GetBlockedUsersAsync(userId);

        return Ok(users);
    }
}