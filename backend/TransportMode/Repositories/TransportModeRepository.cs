using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.TransportMode.Repositories;

public class TransportModeRepository : ITransportModeRepository
{
    private readonly DataContext _context;

    public TransportModeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<TransportMode>> GetAllTransportModesAsync()
    {
        return await _context.Set<TransportMode>()
            .AsNoTracking()
            .OrderBy(x => x.IdTransportMode)
            .ToListAsync();
    }
}