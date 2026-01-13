using Microsoft.AspNetCore.Mvc;

namespace backend.Interest;

[ApiController]
[Route("api/interests")]
public class InterestsController : ControllerBase
{
    private readonly IInterestService _service;

    public InterestsController(IInterestService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var interests = await _service.GetAllAsync();
        return Ok(interests);
    }
}