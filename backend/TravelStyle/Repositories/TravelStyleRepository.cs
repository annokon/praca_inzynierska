using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.TravelStyle.Repositories;

public class TravelStyleRepository : ITravelStyleRepository
{
    private readonly DataContext _context;

    public TravelStyleRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<TravelStyle>> GetAllTravelStylesAsync()
    {
        return await _context.Set<TravelStyle>()
            .AsNoTracking()
            .OrderBy(x => x.IdTravelStyle)
            .ToListAsync();
    }
}