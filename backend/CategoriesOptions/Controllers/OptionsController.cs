using backend.CategoriesOptions.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.CategoriesOptions.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OptionsController : ControllerBase
{
    private readonly IOptionsService _optionsService;

    public OptionsController(IOptionsService optionsService)
    {
        _optionsService = optionsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOptions()
    {
        var options = await _optionsService.GetAllOptionsAsync();
        return Ok(options);
    }
}