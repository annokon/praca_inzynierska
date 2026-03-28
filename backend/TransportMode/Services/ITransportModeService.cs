using backend.TransportMode.DTOs;

namespace backend.TransportMode.Services;

public interface ITransportModeService
{
    Task<List<UserTransportModesDTO>> GetAllTransportModesAsync();
}