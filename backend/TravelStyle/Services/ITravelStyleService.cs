using backend.TravelStyle.DTOs;

namespace backend.TravelStyle.Services;

public interface ITravelStyleService
{
    Task<List<UserTravelStylesDTO>> GetAllTravelStylesAsync();
}