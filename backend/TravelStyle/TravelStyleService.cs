using backend.TravelStyle.DTOs;

namespace backend.TravelStyle;

public class TravelStyleService : ITravelStyleService
{
    private readonly ITravelStyleRepository _repository;

    public TravelStyleService(ITravelStyleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserTravelStylesDTO>> GetAllAsync(string lang)
    {
        var isPl = lang.ToLower() == "pl";

        var data = await _repository.GetAllAsync();

        return data.Select(x => new UserTravelStylesDTO
        {
            Id = x.IdTravelStyle,
            Name = isPl ? x.TravelStyleNamePl : x.TravelStyleNameEn
        }).ToList();
    }
}