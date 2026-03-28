using backend.User.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.User;

[ApiController]
[Route("api/email")]
public class EmailVerificationController : ControllerBase
{
    private readonly IEmailVerificationService _service;

    public EmailVerificationController(IEmailVerificationService service)
    {
        _service = service;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendVerification(ResendVerificationDTO dto)
    {
        var result = await _service.SendVerificationAsync(dto.Email);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Message);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyEmail(VerifyEmailDTO dto)
    {
        var result = await _service.VerifyAsync(dto.Email, dto.Code);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Message);
    }
}
