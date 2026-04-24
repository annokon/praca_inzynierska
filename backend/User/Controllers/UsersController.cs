using System.Security.Claims;
using System.Text.RegularExpressions;
using backend.Security;
using backend.User.DTOs;
using backend.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.User.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    private int GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(claim, out var userId))
            throw new UnauthorizedAccessException("Missing user id claim");

        return int.Parse(claim);
    }
    
    // get all users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }


    // get user by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdUser(int id)
    {
        var user = await _userService.GetByIdUserAsync(id);
        if (user == null)
            return NotFound(new { message = "User not found." });

        return Ok(user);
    }


    // add new user
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var created = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdUser), new { idUser = created.IdUser }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    // update user
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _userService.UpdateAsync(id, dto);
        if (!updated)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }


    // adding optional data to user during registration
    [HttpPut("{id}/additional")]
    public async Task<IActionResult> AddAdditionalData(int id, AdditionalDataUserDTO dto)
    {
        var result = await _userService.AddAdditionalDataAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok();
    }


    // delete user
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO dto)
    {
        var result = await _userService.RegisterAsync(dto);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(new
        {
            message = "New user created",
            id = result.User!.IdUser,
            username = result.User.Username
        });
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDTO dto)
    {
        var result = await _userService.LoginAsync(dto);

        if (!result.Success)
            return Unauthorized(new { message = result.Error });

        CookieHelper.SetAccessToken(Response, result.AccessToken!);
        CookieHelper.SetRefreshToken(Response, result.RefreshToken!);

        return Ok(new { message = "Logged in", role = result.Role });
    }

    
    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        CookieHelper.ClearTokens(Response);
        return Ok(new { message = "Logged out" });
    }


    // my data
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await _userService.GetProfileAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    
    // admin
    [Authorize(Roles = "admin")]
    [HttpGet("admin-only")]
    public IActionResult Admin() => Ok("You are an admin only");

    
    // mod
    [Authorize(Roles = "admin,mod")]
    [HttpGet("staff")]
    public IActionResult Staff() => Ok("You are an admin or moderator");
    
    
    // PATCH /api/users/me
    [HttpPatch("me")]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateUserProfileDTO dto)
    {
        var userId = GetUserId();

        var result = await _userService.UpdateProfileAsync(userId, dto);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(result.User);
    }
    
    [Authorize]
    [HttpPost("me/images")]
    public async Task<IActionResult> UploadImages(IFormFile? profileImage, IFormFile? bannerImage)
    {
        var userId = GetUserId();

        var result = await _userService.UpdateImagesAsync(userId, profileImage, bannerImage);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(result);
    }
    
    [HttpGet("{id}/images")]
    public async Task<IActionResult> GetUserImages(int id)
    {
        var user = await _userService.GetUserImagesAsync(id);

        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(user);
    }
    
    [Authorize]
    [HttpPatch("me/currency")]
    public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrencyDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var result = await _userService.UpdateCurrencyAsync(userId, dto.Currency);

        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "Waluta zaktualizowana" });
    }
    
    [HttpGet("currencies")]
    public async Task<IActionResult> GetCurrencies()
    {
        var currencies = await _userService.GetAvailableCurrenciesAsync();
        return Ok(currencies);
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q, [FromQuery] int limit = 10)
    {
        var users = await _userService.SearchAsync(q, limit);
        return Ok(users);
    }
}