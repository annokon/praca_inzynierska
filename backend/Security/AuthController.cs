using backend.User;
using backend.User.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Security;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly PasswordHasher _hasher;
    private readonly JwtService _jwt;

    public AuthController(IUserRepository users, PasswordHasher hasher, JwtService jwt)
    {
        _users = users;
        _hasher = hasher;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDTO dto)
    {
        var user = await _users.GetByEmailAsync(dto.Email);

        if (user == null || !_hasher.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid credentials" });

        var access = _jwt.GenerateAccessToken(user);
        var refresh = _jwt.GenerateRefreshToken();

        CookieHelper.SetAccessToken(Response, access);
        CookieHelper.SetRefreshToken(Response, refresh);

        return Ok(new { message = "Logged in", role = user.Role });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Username) ||
            string.IsNullOrWhiteSpace(dto.DisplayName) ||
            string.IsNullOrWhiteSpace(dto.Password))
        {
            return BadRequest(new { message = "Wszystkie pola są wymagane." });
        }
        
        if (!dto.Email.Contains('@') || !dto.Email.Contains('.'))
            return BadRequest(new { message = "Podaj poprawny adres email." });
        
        if (await _users.ExistsByEmailAsync(dto.Email))
            return BadRequest(new { message = "Ten email jest już zajęty." });

        if (await _users.ExistsByUsernameAsync(dto.Username))
            return BadRequest(new { message = "Ta nazwa użytkownika jest już zajęta." });
        
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var minBirth = today.AddYears(-16);

        if (dto.BirthDate > minBirth)
            return BadRequest(new { message = "Musisz mieć co najmniej 16 lat, aby założyć konto." });
        
        if (dto.Password.Length < 8)
            return BadRequest(new { message = "Hasło musi mieć co najmniej 8 znaków." });
        
        var user = new backend.Models.User
        {
            Username = dto.Username.Trim(),
            DisplayName = dto.DisplayName.Trim(),
            Email = dto.Email.Trim(),
            BirthDate = dto.BirthDate,
            PasswordHash = _hasher.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            
            Role = "user",
            Currency = "PLN",
            SystemLanguage = "pl-PL",

            IsActive = true
        };

        await _users.AddAsync(user);

        return Ok(new
        {
            message = "Utworzono użytkownika.",
            id = user.IdUser,
            username = user.Username
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        CookieHelper.ClearTokens(Response);
        return Ok(new { message = "Logged out" });
    }
}