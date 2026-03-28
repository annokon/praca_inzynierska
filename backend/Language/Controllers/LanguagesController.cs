using backend.Language.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Language.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController : ControllerBase
{
    private readonly ILanguageService _service;

    public LanguagesController(ILanguageService languageService)
    {
        _service = languageService;
    }

    [Authorize]
    [HttpGet("{id}/languages")]
    public async Task<IActionResult> GetUserLanguages(int id)
    {
        var langs = await _service.GetUserLanguagesAsync(id);
        return Ok(langs);
    }

    [Authorize]
    [HttpPut("{id}/languages")]
    public async Task<IActionResult> UpdateUserLanguages(int id, [FromBody] List<int> languageIds)
    {
        bool updated = await _service.UpdateUserLanguagesAsync(id, languageIds);
        if (!updated) return NotFound();

        return Ok(new { message = "Languages updated." });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLanguages()
    {
        var result = await _service.GetAllLanguagesAsync();
        return Ok(result);
    }
}