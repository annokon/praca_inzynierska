using backend.User.Models;

namespace backend.User.Repositories;

public interface IBlockedUserRepository
{
    Task<bool> ExistsAsync(int blockerId, int blockedId);
    Task<BlockedUser?> GetAsync(int blockerId, int blockedId);
    Task AddAsync(BlockedUser blockedUser);
    Task RemoveAsync(BlockedUser blockedUser);
    Task<List<Models.User>> GetBlockedUsersAsync(int userId);
    Task<bool> ExistsEitherWay(int userA, int userB);
    Task<HashSet<int>> GetBlockedUserIdsAsync(int userId);
}