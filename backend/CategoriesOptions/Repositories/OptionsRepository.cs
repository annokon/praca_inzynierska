using backend.CategoriesOptions.DTOs;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.CategoriesOptions.Repositories;

public class OptionsRepository : IOptionsRepository
{
    private readonly DataContext _context;

    public OptionsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDTO>> GetAlcoholPreferences()
    {
        return await _context.AlcoholPreferenceOptions
            .Select(x => new OptionDTO
            {
                Id = x.IdAlcoholPreference,
                Name = x.AlcoholPreferenceName
            })
            .ToListAsync();
    }

    public async Task<List<OptionDTO>> GetGenders()
    {
        return await _context.GenderOptions
            .Select(x => new OptionDTO
            {
                Id = x.IdGender,
                Name = x.GenderName
            })
            .ToListAsync();
    }

    public async Task<List<OptionDTO>> GetPronouns()
    {
        return await _context.PronounOptions
            .Select(x => new OptionDTO
            {
                Id = x.IdPronoun,
                Name = x.PronounName
            })
            .ToListAsync();
    }

    public async Task<List<OptionDTO>> GetPersonalityTypes()
    {
        return await _context.PersonalityTypeOptions
            .Select(x => new OptionDTO
            {
                Id = x.IdPersonalityType,
                Name = x.PersonalityTypeName
            })
            .ToListAsync();
    }

    public async Task<List<OptionDTO>> GetSmokingPreferences()
    {
        return await _context.SmokingPreferenceOptions
            .Select(x => new OptionDTO
            {
                Id = x.IdSmokingPreference,
                Name = x.SmokingPreferenceName
            })
            .ToListAsync();
    }

    public async Task<List<OptionDTO>> GetDrivingLicenses()
    {
        return await _context.DrivingLicenseOptions
            .Select(x => new OptionDTO
            {
                Id = x.IdDrivingLicense,
                Name = x.DrivingLicenseName
            })
            .ToListAsync();
    }

    public async Task<List<OptionDTO>> GetTravelExperiences()
    {
        return await _context.TravelExperienceOptions
            .Select(x => new OptionDTO
            {
                Id = x.IdTravelExperience,
                Name = x.TravelExperienceName
            })
            .ToListAsync();
    }
}