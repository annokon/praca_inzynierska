namespace backend.TravelStyle.Repositories;

public interface ITravelStyleRepository
{
    Task<List<TravelStyle>> GetAllTravelStylesAsync();
}