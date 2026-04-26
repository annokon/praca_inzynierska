using backend.User.DTOs;

namespace backend.User.Services;

public interface IBlockedUserService
{
    Task<(bool Success, string? Error)> BlockAsync(int userId, int blockedUserId);
    Task<(bool Success, string? Error)> UnblockAsync(int userId, int blockedUserId);
    Task<List<UserSearchDTO>> GetBlockedUsersAsync(int userId);
}