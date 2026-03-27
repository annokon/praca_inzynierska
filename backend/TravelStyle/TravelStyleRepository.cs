using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.TravelStyle;

public class TravelStyleRepository : ITravelStyleRepository
{
    private readonly DataContext _context;

    public TravelStyleRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Models.TravelStyle>> GetAllAsync()
    {
        return await _context.Set<Models.TravelStyle>()
            .AsNoTracking()
            .OrderBy(x => x.IdTravelStyle)
            .ToListAsync();
    }
}