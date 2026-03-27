using backend.TransportMode.DTOs;

namespace backend.TransportMode;

public interface ITransportModeService
{
    Task<List<UserTransportModesDTO>> GetAllAsync(string lang);
}