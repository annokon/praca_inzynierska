namespace backend.TransportMode.Repositories;

public interface ITransportModeRepository
{
    Task<List<TransportMode>> GetAllTransportModesAsync();
}