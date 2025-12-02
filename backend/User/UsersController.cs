using System.Security.Claims;
using backend.DTOs;
using backend.Security;
using backend.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.User;

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
    public async Task<IActionResult> AddAdditionalData(int id, [FromBody] AdditionalDataUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _userService.AddAdditionalDataAsync(id, dto);
        if (!updated)
            return NotFound(new { message = "User not found." });

        return NoContent();
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
        var user = await _userService.GetByIdAsync(id);

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
    
    
    
    [Authorize]
    [HttpGet("{id}/languages")]
    public async Task<IActionResult> GetUserLanguages(int id)
    {
        var langs = await _userService.GetUserLanguagesAsync(id);
        return Ok(langs);
    }

    [Authorize]
    [HttpPut("{id}/languages")]
    public async Task<IActionResult> UpdateUserLanguages(int id, [FromBody] List<int> languageIds)
    {
        bool updated = await _userService.UpdateUserLanguagesAsync(id, languageIds);
        if (!updated) return NotFound();

        return Ok(new { message = "Languages updated." });
    }

    [HttpGet("languages")]
    public async Task<IActionResult> GetAllLanguages()
    {
        var langs = await _userService.GetAllLanguagesAsync();
        return Ok(langs);
    }

}