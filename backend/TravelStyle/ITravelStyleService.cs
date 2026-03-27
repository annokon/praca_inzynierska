using backend.TravelStyle.DTOs;

namespace backend.TravelStyle;

public interface ITravelStyleService
{
    Task<List<UserTravelStylesDTO>> GetAllAsync(string lang);
}