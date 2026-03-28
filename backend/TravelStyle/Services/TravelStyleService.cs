using backend.TravelStyle.DTOs;
using backend.TravelStyle.Repositories;

namespace backend.TravelStyle.Services;

public class TravelStyleService : ITravelStyleService
{
    private readonly ITravelStyleRepository _repository;

    public TravelStyleService(ITravelStyleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserTravelStylesDTO>> GetAllTravelStylesAsync()
    {
        var data = await _repository.GetAllTravelStylesAsync();

        return data.Select(x => new UserTravelStylesDTO
        {
            Id = x.IdTravelStyle,
            Name = x.TravelStyleName
        }).ToList();
    }
}