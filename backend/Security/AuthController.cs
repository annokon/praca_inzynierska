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
        var user = (await _users.GetAllAsync())
            .FirstOrDefault(u => u.Email == dto.Email);

        if (user == null || !_hasher.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid credentials" });

        var access = _jwt.GenerateAccessToken(user);
        var refresh = _jwt.GenerateRefreshToken();

        CookieHelper.SetAccessToken(Response, access);
        CookieHelper.SetRefreshToken(Response, refresh);

        return Ok(new { message = "Logged in", role = user.Role });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        CookieHelper.ClearTokens(Response);
        return Ok(new { message = "Logged out" });
    }
}