using backend.CategoriesOptions.Repositories;

namespace backend.CategoriesOptions.Services;

public class OptionsService : IOptionsService
{
    private readonly IOptionsRepository _repository;

    public OptionsService(IOptionsRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> GetAllOptionsAsync()
    {
        var genders = await _repository.GetGenders();
        var pronouns = await _repository.GetPronouns();
        var personalities = await _repository.GetPersonalityTypes();
        var alcohol = await _repository.GetAlcoholPreferences();
        var smoking = await _repository.GetSmokingPreferences();
        var driving = await _repository.GetDrivingLicenses();
        var travelExperience = await _repository.GetTravelExperiences();

        return new
        {
            genders,
            pronouns,
            personalities,
            alcohol,
            smoking,
            driving,
            travelExperience
        };
    }
}