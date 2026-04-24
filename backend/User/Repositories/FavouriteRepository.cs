using backend.Infrastructure.Data;
using backend.User.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.User.Repositories;

public class FavouriteRepository : IFavouriteRepository
{
    private readonly DataContext _context;

    public FavouriteRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(int userId, int favouriteUserId)
    {
        return await _context.Favourites
            .AnyAsync(f => f.IdUser == userId && f.IdUserFavourite == favouriteUserId);
    }

    public async Task<Favourite?> GetAsync(int userId, int favouriteUserId)
    {
        return await _context.Favourites
            .FirstOrDefaultAsync(f => f.IdUser == userId && f.IdUserFavourite == favouriteUserId);
    }

    public async Task AddAsync(Favourite favourite)
    {
        _context.Favourites.Add(favourite);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Favourite favourite)
    {
        _context.Favourites.Remove(favourite);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Models.User>> GetUserFavouritesAsync(int userId)
    {
        return await _context.Favourites
            .Where(f => f.IdUser == userId)
            .Select(f => f.FavouriteUser)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}