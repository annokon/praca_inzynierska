using backend.DTOs;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

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


    // get user by username
    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
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
            return CreatedAtAction(nameof(GetByUsername), new { username = created.Username }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    // update user
    [HttpPut("{username}")]
    public async Task<IActionResult> Update(string username, [FromBody] UpdateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _userService.UpdateAsync(username, dto);
        if (!updated)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }


    // adding optional data to user during registration
    [HttpPut("{username}/additional")]
    public async Task<IActionResult> AddAdditionalData(string username, [FromBody] AdditionalDataUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _userService.AddAdditionalDataAsync(username, dto);
        if (!updated)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }


    // delete user
    [HttpDelete("{username}")]
    public async Task<IActionResult> Delete(string username)
    {
        var deleted = await _userService.DeleteAsync(username);
        if (!deleted)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }
}