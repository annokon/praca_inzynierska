using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Language;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController : ControllerBase
{
    private readonly ILanguageService _languageService;

    public LanguagesController(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    [Authorize]
    [HttpGet("{id}/languages")]
    public async Task<IActionResult> GetUserLanguages(int id)
    {
        var langs = await _languageService.GetUserLanguagesAsync(id);
        return Ok(langs);
    }

    [Authorize]
    [HttpPut("{id}/languages")]
    public async Task<IActionResult> UpdateUserLanguages(int id, [FromBody] List<int> languageIds)
    {
        bool updated = await _languageService.UpdateUserLanguagesAsync(id, languageIds);
        if (!updated) return NotFound();

        return Ok(new { message = "Languages updated." });
    }

    [HttpGet("languages")]
    public async Task<IActionResult> GetAllLanguages()
    {
        var langs = await _languageService.GetAllLanguagesAsync();
        return Ok(langs);
    }
    
    [HttpGet("languages/pl")]
    public async Task<IActionResult> GetAllLanguagesPL()
    {
        var langs = await _languageService.GetAllLanguagesPlAsync();
        return Ok(langs);
    }

    [HttpGet("languages/en")]
    public async Task<IActionResult> GetAllLanguagesEN()
    {
        var langs = await _languageService.GetAllLanguagesEnAsync();
        return Ok(langs);
    }
}