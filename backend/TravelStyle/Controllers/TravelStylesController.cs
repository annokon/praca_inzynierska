using backend.TransportMode;
using backend.TravelStyle.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.TravelStyle.Controllers;

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
    public async Task<IActionResult> GetAllTravelStyles()
    {
        var result = await _service.GetAllTravelStylesAsync();
        return Ok(result);
    }
}