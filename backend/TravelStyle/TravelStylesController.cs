using backend.TransportMode;
using Microsoft.AspNetCore.Mvc;

namespace backend.TravelStyle;

[ApiController]
[Route("api/[controller]")]
public class TravelStylesController : ControllerBase
{
    private readonly ITravelStyleService _service;

    public TravelStylesController(ITravelStyleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string lang = "en")
    {
        var result = await _service.GetAllAsync(lang);
        return Ok(result);
    }
}