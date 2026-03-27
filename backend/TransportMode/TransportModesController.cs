using Microsoft.AspNetCore.Mvc;

namespace backend.TransportMode;

[ApiController]
[Route("api/[controller]")]
public class TransportModesController : ControllerBase
{
    private readonly ITransportModeService _service;

    public TransportModesController(ITransportModeService service)
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