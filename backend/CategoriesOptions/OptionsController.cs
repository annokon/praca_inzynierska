using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Data;
using backend.Options;

namespace backend.Options;

[ApiController]
[Route("api/[controller]")]
public class OptionsController : ControllerBase
{
    private readonly DataContext _context;

    public OptionsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOptions([FromQuery] string lang = "en")
    {
        bool isPl = lang.ToLower() == "pl";

        var genders = await GetGenders(isPl);
        var pronouns = await GetPronouns(isPl);
        var personalities = await GetPersonalityTypes(isPl);
        var alcohol = await GetAlcoholPreferences(isPl);
        var smoking = await GetSmokingPreferences(isPl);
        var driving = await GetDrivingLicenses(isPl);
        var travelExperience = await GetTravelExperiences(isPl);
        // var transportModes = await GetTransportModes(isPl);  TODO

        return Ok(new
        {
            genders,
            pronouns,
            personalities,
            alcohol,
            smoking,
            driving,
            travelExperience,
            // transportModes   TODO
        });
    }

    private async Task<List<OptionDTO>> GetGenders(bool isPl)
    {
        return await _context.GenderOptions
            .Select(x => new OptionDTO { Id = x.Id, Name = isPl ? x.NamePl : x.NameEn })
            .ToListAsync();
    }

    private async Task<List<OptionDTO>> GetPronouns(bool isPl)
    {
        return await _context.PronounOptions
            .Select(x => new OptionDTO { Id = x.Id, Name = isPl ? x.NamePl : x.NameEn })
            .ToListAsync();
    }

    private async Task<List<OptionDTO>> GetPersonalityTypes(bool isPl)
    {
        return await _context.PersonalityTypeOptions
            .Select(x => new OptionDTO { Id = x.Id, Name = isPl ? x.NamePl : x.NameEn })
            .ToListAsync();
    }

    private async Task<List<OptionDTO>> GetAlcoholPreferences(bool isPl)
    {
        return await _context.AlcoholPreferenceOptions
            .Select(x => new OptionDTO { Id = x.Id, Name = isPl ? x.NamePl : x.NameEn })
            .ToListAsync();
    }

    private async Task<List<OptionDTO>> GetSmokingPreferences(bool isPl)
    {
        return await _context.SmokingPreferenceOptions
            .Select(x => new OptionDTO { Id = x.Id, Name = isPl ? x.NamePl : x.NameEn })
            .ToListAsync();
    }

    private async Task<List<OptionDTO>> GetDrivingLicenses(bool isPl)
    {
        return await _context.DrivingLicenseOptions
            .Select(x => new OptionDTO { Id = x.Id, Name = isPl ? x.NamePl : x.NameEn })
            .ToListAsync();
    }

    private async Task<List<OptionDTO>> GetTravelExperiences(bool isPl)
    {
        return await _context.TravelExperienceOptions
            .Select(x => new OptionDTO { Id = x.Id, Name = isPl ? x.NamePl : x.NameEn })
            .ToListAsync();
    }

    // TODO
    // private async Task<List<OptionDTO>> GetTransportModes(bool isPl)
    // {
    //     return 
    // }
}