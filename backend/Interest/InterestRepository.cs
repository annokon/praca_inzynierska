using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Interest;

public class InterestRepository : IInterestRepository
{
    private readonly DataContext _context;

    public InterestRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Interest>> GetAllAsync() =>
        await _context.Set<Models.Interest>()
            .AsNoTracking()
            .OrderBy(i => i.IdInterest)
            .ToListAsync();

    public async Task<bool> AnyAsync() =>
        await _context.Interests.AnyAsync();

    public async Task<bool> ExistsByNameAsync(string name) =>
        await _context.Interests.AnyAsync(i => i.InterestName == name);

    public async Task AddAsync(Models.Interest interest)
    {
        _context.Interests.Add(interest);
        await _context.SaveChangesAsync();
    }
}