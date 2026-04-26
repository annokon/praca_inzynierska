using backend.User.DTOs;
using backend.User.Models;
using backend.User.Repositories;

namespace backend.User.Services;

public class BlockedUserService : IBlockedUserService
{
    private readonly IBlockedUserRepository _repo;
    private readonly IUserRepository _userRepo;
    private readonly IFavouriteRepository _favRepo;

    public BlockedUserService(
        IBlockedUserRepository repo,
        IUserRepository userRepo,
        IFavouriteRepository favRepo)
    {
        _repo = repo;
        _userRepo = userRepo;
        _favRepo = favRepo;
    }

    public async Task<(bool Success, string? Error)> BlockAsync(int userId, int blockedUserId)
    {
        if (userId == blockedUserId)
            return (false, "You cannot block yourself.");

        var userExists = await _userRepo.GetByIdUserAsync(blockedUserId);
        if (userExists == null)
            return (false, "User does not exist.");

        var exists = await _repo.ExistsAsync(userId, blockedUserId);
        if (exists)
            return (false, "User already blocked.");
        
        var fav = await _favRepo.GetAsync(userId, blockedUserId);
        if (fav != null)
            await _favRepo.RemoveAsync(fav);

        var blocked = new BlockedUser
        {
            IdUserBlocker = userId,
            IdUserBlocked = blockedUserId
        };

        await _repo.AddAsync(blocked);

        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UnblockAsync(int userId, int blockedUserId)
    {
        var blocked = await _repo.GetAsync(userId, blockedUserId);

        if (blocked == null)
            return (false, "Block does not exist.");

        await _repo.RemoveAsync(blocked);

        return (true, null);
    }

    public async Task<List<UserSearchDTO>> GetBlockedUsersAsync(int userId)
    {
        var users = await _repo.GetBlockedUsersAsync(userId);

        return users.Select(u => new UserSearchDTO
        {
            Id = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            ProfilePhotoPath = u.ProfilePhotoPath
        }).ToList();
    }
}