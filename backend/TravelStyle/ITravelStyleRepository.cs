namespace backend.TravelStyle;

public interface ITravelStyleRepository
{
    Task<List<Models.TravelStyle>> GetAllAsync();
}