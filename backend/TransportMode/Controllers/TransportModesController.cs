using backend.TransportMode.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.TransportMode.Controllers;

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
    public async Task<IActionResult> GetAllTransportModes()
    {
        var result = await _service.GetAllTransportModesAsync();
        return Ok(result);
    }
}