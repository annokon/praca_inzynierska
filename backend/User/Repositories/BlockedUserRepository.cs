using backend.Infrastructure.Data;
using backend.User.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.User.Repositories;

public class BlockedUserRepository : IBlockedUserRepository
{
    private readonly DataContext _context;

    public BlockedUserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(int blockerId, int blockedId)
    {
        return await _context.BlockedUsers
            .AnyAsync(b => b.IdUserBlocker == blockerId && b.IdUserBlocked == blockedId);
    }

    public async Task<BlockedUser?> GetAsync(int blockerId, int blockedId)
    {
        return await _context.BlockedUsers
            .FirstOrDefaultAsync(b => b.IdUserBlocker == blockerId && b.IdUserBlocked == blockedId);
    }

    public async Task AddAsync(BlockedUser blockedUser)
    {
        _context.BlockedUsers.Add(blockedUser);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(BlockedUser blockedUser)
    {
        _context.BlockedUsers.Remove(blockedUser);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Models.User>> GetBlockedUsersAsync(int userId)
    {
        return await _context.BlockedUsers
            .Where(b => b.IdUserBlocker == userId)
            .Select(b => b.Blocked)
            .ToListAsync();
    }

    public async Task<bool> ExistsEitherWay(int userA, int userB)
    {
        return await _context.BlockedUsers
            .AnyAsync(b =>
                (b.IdUserBlocker == userA && b.IdUserBlocked == userB) ||
                (b.IdUserBlocker == userB && b.IdUserBlocked == userA)
            );
    }
    
    public async Task<HashSet<int>> GetBlockedUserIdsAsync(int userId)
    {
        return await _context.BlockedUsers
            .Where(b => b.IdUserBlocker == userId || b.IdUserBlocked == userId)
            .Select(b => b.IdUserBlocker == userId ? b.IdUserBlocked : b.IdUserBlocker)
            .ToHashSetAsync();
    }
}