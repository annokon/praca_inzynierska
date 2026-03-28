using backend.CategoriesOptions.DTOs;

namespace backend.CategoriesOptions.Repositories;

public interface IOptionsRepository
{
    Task<List<OptionDTO>> GetGenders();
    Task<List<OptionDTO>> GetPronouns();
    Task<List<OptionDTO>> GetPersonalityTypes();
    Task<List<OptionDTO>> GetAlcoholPreferences();
    Task<List<OptionDTO>> GetSmokingPreferences();
    Task<List<OptionDTO>> GetDrivingLicenses();
    Task<List<OptionDTO>> GetTravelExperiences();
}