using backend.User.Models;

namespace backend.User.Repositories;

public interface IFavouriteRepository
{
    Task<bool> ExistsAsync(int userId, int favouriteUserId);
    Task AddAsync(Favourite favourite);
    Task RemoveAsync(Favourite favourite);
    Task<Favourite?> GetAsync(int userId, int favouriteUserId);
    Task<List<Models.User>> GetUserFavouritesAsync(int userId);
    Task SaveChangesAsync();
}