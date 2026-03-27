using backend.TransportMode.DTOs;

namespace backend.TransportMode;

public class TransportModeService : ITransportModeService
{
    private readonly ITransportModeRepository _repository;

    public TransportModeService(ITransportModeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserTransportModesDTO>> GetAllAsync(string lang)
    {
        var isPl = lang.ToLower() == "pl";

        var data = await _repository.GetAllAsync();

        return data.Select(x => new UserTransportModesDTO
        {
            Id = x.IdTransportMode,
            Name = isPl ? x.TransportModeNamePl : x.TransportModeNameEn
        }).ToList();
    }
}