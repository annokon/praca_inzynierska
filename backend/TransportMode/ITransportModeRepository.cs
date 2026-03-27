namespace backend.TransportMode;

public interface ITransportModeRepository
{
    Task<List<Models.TransportMode>> GetAllAsync();
}