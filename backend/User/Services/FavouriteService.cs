using backend.User.DTOs;
using backend.User.Models;
using backend.User.Repositories;

namespace backend.User.Services;

public class FavouriteService : IFavouriteService
{
    private readonly IFavouriteRepository _repo;
    private readonly IUserRepository _userRepo;
    private readonly IBlockedUserRepository _blockedRepo;

    public FavouriteService(IFavouriteRepository repo, IUserRepository userRepo, IBlockedUserRepository blockedRepo)
    {
        _repo = repo;
        _userRepo = userRepo;
        _blockedRepo = blockedRepo;
    }
    
    public async Task<(bool Success, string? Error)> AddAsync(int userId, int favouriteUserId)
    {
        if (userId == favouriteUserId)
            return (false, "You cannot favourite yourself.");

        var userExists = await _userRepo.GetByIdUserAsync(favouriteUserId);
        if (userExists == null)
            return (false, "User does not exist.");

        var exists = await _repo.ExistsAsync(userId, favouriteUserId);
        if (exists)
            return (false, "Already in favourites.");
        
        var isBlocked = await _blockedRepo.ExistsEitherWay(userId, favouriteUserId);
        if (isBlocked)
            return (false, "User is blocked.");
        
        var fav = new Favourite
        {
            IdUser = userId,
            IdUserFavourite = favouriteUserId
        };

        await _repo.AddAsync(fav);

        return (true, null);
    }

    public async Task<(bool Success, string? Error)> RemoveAsync(int userId, int favouriteUserId)
    {
        var fav = await _repo.GetAsync(userId, favouriteUserId);

        if (fav == null)
            return (false, "Favourite does not exist.");

        await _repo.RemoveAsync(fav);

        return (true, null);
    }

    public async Task<List<UserSearchDTO>> GetUserFavouritesAsync(int userId)
    {
        var users = await _repo.GetUserFavouritesAsync(userId);

        return users.Select(u => new UserSearchDTO
        {
            Id = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            ProfilePhotoPath = u.ProfilePhotoPath
        }).ToList();
    }
}