using backend.TransportMode.DTOs;
using backend.TransportMode.Repositories;

namespace backend.TransportMode.Services;

public class TransportModeService : ITransportModeService
{
    private readonly ITransportModeRepository _repository;

    public TransportModeService(ITransportModeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserTransportModesDTO>> GetAllTransportModesAsync()
    {
        var data = await _repository.GetAllTransportModesAsync();

        return data.Select(x => new UserTransportModesDTO
        {
            Id = x.IdTransportMode,
            Name = x.TransportModeName
        }).ToList();
    }
}