using backend.User.DTOs;

namespace backend.User.Services;

public interface  IFavouriteService
{
    Task<(bool Success, string? Error)> AddAsync(int userId, int favouriteUserId);
    Task<(bool Success, string? Error)> RemoveAsync(int userId, int favouriteUserId);
    Task<List<UserSearchDTO>> GetUserFavouritesAsync(int userId);
}