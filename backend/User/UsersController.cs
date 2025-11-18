using System.Security.Claims;
using backend.DTOs;
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
    public async Task<IActionResult> GetByIdUser(int idUser)
    {
        var user = await _userService.GetByIdUserAsync(idUser);
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
    public async Task<IActionResult> Update(int idUser, [FromBody] UpdateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _userService.UpdateAsync(idUser, dto);
        if (!updated)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }


    // adding optional data to user during registration
    [HttpPut("{id}/additional")]
    public async Task<IActionResult> AddAdditionalData(int idUser, [FromBody] AdditionalDataUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _userService.AddAdditionalDataAsync(idUser, dto);
        if (!updated)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }


    // delete user
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int idUser)
    {
        var deleted = await _userService.DeleteAsync(idUser);
        if (!deleted)
            return NotFound(new { message = "User not found." });

        return NoContent();
    }
    
    // only admin
    [Authorize(Roles = "admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminAction() => Ok("Jesteś adminem");

    // only mod and admin
    [Authorize(Roles = "admin,mod")]
    [HttpGet("staff")]
    public IActionResult StaffAction() => Ok("Jesteś adminem albo moderatorem");

    // only logged user
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyData()
    {
        var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _userService.GetByIdUserAsync(id);
        return Ok(user);
    }
}