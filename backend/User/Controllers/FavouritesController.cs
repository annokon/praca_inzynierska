using System.Security.Claims;
using backend.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.User.Controllers;

[Route("api/favourites")]
[ApiController]
[Authorize]
public class FavouritesController : ControllerBase
{
    private readonly IFavouriteService _service;

    public FavouritesController(IFavouriteService service)
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

    /// <summary>
    /// Adds a user to the current user's favourites.
    /// </summary>
    /// <param name="id">ID of the user to favourite.</param>
    /// <returns>Operation result.</returns>
    [HttpPost("{id}")]
    public async Task<IActionResult> Add(int id)
    {
        var userId = GetUserId();

        var result = await _service.AddAsync(userId, id);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "User added to favourites" });
    }

    /// <summary>
    /// Removes a user from the current user's favourites.
    /// </summary>
    /// <param name="id">ID of the user to remove from favourites.</param>
    /// <returns>Operation result.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var userId = GetUserId();

        var result = await _service.RemoveAsync(userId, id);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "User removed from favourites" });
    }

    /// <summary>
    /// Retrieves a list of users favourited by the current user.
    /// </summary>
    /// <returns>List of favourited users.</returns>
    [HttpGet]
    public async Task<IActionResult> GetMyFavourites()
    {
        var userId = GetUserId();

        var users = await _service.GetUserFavouritesAsync(userId);

        return Ok(users);
    }
}