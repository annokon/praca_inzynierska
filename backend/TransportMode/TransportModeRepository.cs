using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.TransportMode;

public class TransportModeRepository : ITransportModeRepository
{
    private readonly DataContext _context;

    public TransportModeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Models.TransportMode>> GetAllAsync()
    {
        return await _context.Set<Models.TransportMode>()
            .AsNoTracking()
            .OrderBy(x => x.IdTransportMode)
            .ToListAsync();
    }
}