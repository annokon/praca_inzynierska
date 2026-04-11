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
    
    
    // update display name
    [Authorize]
    [HttpPut("display-name")]
    public async Task<IActionResult> UpdateDisplayName([FromBody] UpdateDisplayNameDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.DisplayName))
            return BadRequest(new { message = "Nazwa nie może być pusta." });

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var (success, error, user) = await _userService.UpdateDisplayNameAsync(userId, dto.DisplayName);

        if (!success)
            return BadRequest(new { message = error });

        return Ok(user);
    }
    
    // update username
    [Authorize]
    [HttpPut("username")]
    public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username))
            return BadRequest(new { message = "Nazwa użytkownika nie może być pusta." });

        var normalizedUsername = dto.Username.Trim().ToLower();
        
        if (normalizedUsername.Length < 3)
            return BadRequest(new { message = "Nazwa użytkownika musi mieć co najmniej 3 znaki." });

        if (normalizedUsername.Length > 20)
            return BadRequest(new { message = "Nazwa użytkownika nie może być dłuższa niż 20 znaków." });
        
        if (!Regex.IsMatch(normalizedUsername, "^[a-z0-9_]+$"))
            return BadRequest(new { message = "Nazwa użytkownika może zawierać tylko litery, cyfry i podkreślniki." });

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var (success, error, user) = await _userService.UpdateUsernameAsync(userId, normalizedUsername);

        if (!success)
            return BadRequest(new { message = error });

        return Ok(user);
    }
    
    // update birthdate
    [Authorize]
    [HttpPut("birth-date")]
    public async Task<IActionResult> UpdateBirthDate([FromBody] UpdateBirthDateDTO dto)
    {
        if (dto.BirthDate == default)
            return BadRequest(new { message = "Birth date is required." });

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var (success, error, user) = await _userService.UpdateBirthDateAsync(userId, dto.BirthDate);

        if (!success)
            return BadRequest(new { message = error });

        return Ok(user);
    }
    
    // update gender
    [Authorize]
    [HttpPut("gender")]
    public async Task<IActionResult> UpdateGender([FromBody] UpdateGenderDTO dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        var result = await _userService.UpdateGenderAsync(userId, dto.GenderId);

        if (!result.Success)
            return BadRequest(new { error = result.Error });

        return Ok(result.User);
    }
    
    // update languages
    [Authorize]
    [HttpPut("languages")]
    public async Task<IActionResult> UpdateLanguages([FromBody] UpdateLanguagesDTO dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var (success, error) = await _userService.UpdateLanguagesAsync(userId, dto.LanguageIds);

        if (!success)
            return BadRequest(new { message = error });

        return Ok(new { message = "Languages updated." });
    }
    
    // update pronouns
    [Authorize]
    [HttpPut("pronouns")]
    public async Task<IActionResult> UpdatePronouns([FromBody] UpdatePronounsDTO dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        var result = await _userService.UpdatePronounsAsync(userId, dto.PronounsId);

        if (!result.Success)
            return BadRequest(new { error = result.Error });

        return Ok(result.User);
    }
    
    // update bio
    [Authorize]
    [HttpPut("aboutme")]
    public async Task<IActionResult> UpdateBio([FromBody] UpdateBioDTO dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var (success, error, user) = await _userService.UpdateBioAsync(userId, dto.AboutMe);

        if (!success)
            return BadRequest(new { message = error });

        return Ok(user);
    }
    
    
}