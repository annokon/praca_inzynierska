using backend.Infrastructure.Data;
using backend.User.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.User.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }


    // get all users
    Task<IEnumerable<Models.User>> IUserRepository.GetAllAsync()
    {
        return GetAllAsync();
    }

    public async Task<IEnumerable<Models.User>> GetAllAsync() =>
        await _context.Users.AsNoTracking().ToListAsync();


    // get user by id
    Task<Models.User?> IUserRepository.GetByIdUserAsync(int idUser)
    {
        return GetByIdUserAsync(idUser);
    }

    public async Task<Models.User?> GetByIdUserAsync(int idUser) =>
        await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);


    // add new user
    Task IUserRepository.AddAsync(Models.User user)
    {
        return AddAsync(user);
    }

    public async Task AddAsync(Models.User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }


    // update user
    Task IUserRepository.UpdateAsync(Models.User user)
    {
        return UpdateAsync(user);
    }

    public async Task UpdateAsync(Models.User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }


    // delete user
    Task IUserRepository.DeleteAsync(Models.User user)
    {
        return DeleteAsync(user);
    }

    public async Task DeleteAsync(Models.User user)
    {
        //TODO
    }


    // does any user with this email already exist?
    public async Task<bool> ExistsByEmailAsync(string email) =>
        await _context.Users.AnyAsync(u => u.Email == email);

    // does any user with this username already exist?
    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _context.Users.AnyAsync(u => u.Username == username);


    public async Task<Models.User?> GetByEmailOrUsernameAsync(string login)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == login || u.Username == login);
    }

    public async Task<Models.User?> GetUserWithLanguagesAsync(int idUser)
    {
        return await _context.Users
            .Include(u => u.UserLanguages)!
            .ThenInclude(ul => ul.Language)
            .FirstOrDefaultAsync(u => u.IdUser == idUser);
    }

    public async Task<bool> ValidateGender(int dtoGenderId) =>
        await _context.GenderOptions.AnyAsync(g => g.IdGender == dtoGenderId);

    public async Task<Models.User?> GetUserWithRelationsAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Gender)
            .Include(u => u.Pronouns)
            .Include(u => u.PersonalityType)
            .Include(u => u.TravelExperience)
            .Include(u => u.DrivingLicense)
            .Include(u => u.AlcoholPreference)
            .Include(u => u.SmokingPreference)
            .Include(u => u.UserLanguages)!
            .ThenInclude(ul => ul.Language)
            .Include(u => u.UserInterests)
            .ThenInclude(ui => ui.Interest)
            .Include(u => u.UserTravelStyles)
            .ThenInclude(uts => uts.TravelStyle)
            .Include(u => u.UserTransportModes)
            .ThenInclude(ut => ut.TransportMode)
            .FirstOrDefaultAsync(u => u.IdUser == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ValidatePersonalityType(int personalityId) =>
        await _context.PersonalityTypeOptions.AnyAsync(p => p.IdPersonalityType == personalityId);

    public async Task<bool> ValidateAlcoholPreference(int alcoholId) =>
        await _context.AlcoholPreferenceOptions.AnyAsync(a => a.IdAlcoholPreference == alcoholId);

    public async Task<bool> ValidateSmokingPreference(int smokingId) =>
        await _context.SmokingPreferenceOptions.AnyAsync(s => s.IdSmokingPreference == smokingId);

    public async Task<bool> ValidatePronouns(int pronounsId) =>
        await _context.PronounOptions.AnyAsync(p => p.IdPronoun == pronounsId);

    public async Task<bool> ValidateDrivingLicense(int drivingLicenseId) =>
        await _context.DrivingLicenseOptions.AnyAsync(d => d.IdDrivingLicense == drivingLicenseId);

    public async Task<bool> ValidateTravelExperience(int travelExperienceId) =>
        await _context.TravelExperienceOptions.AnyAsync(t => t.IdTravelExperience == travelExperienceId);

    public async Task<bool> ValidateInterest(int id) =>
        await _context.Interests.AnyAsync(i => i.IdInterest == id);

    public async Task<bool> ValidateTravelStyle(int id) =>
        await _context.TravelStyles.AnyAsync(t => t.IdTravelStyle == id);

    public async Task<bool> ValidateTransportMode(int id) =>
        await _context.TransportModes.AnyAsync(t => t.IdTransportMode == id);

    public async Task<bool> ValidateLanguages(List<int> ids)
    {
        var distinct = ids.Distinct().ToList();

        var count = await _context.Languages
            .CountAsync(l => distinct.Contains(l.IdLanguage));

        return count == distinct.Count;
    }

    public async Task<bool> UpdateCurrencyAsync(int userId, string currency)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == userId);
        if (user == null) return false;

        user.Currency = currency;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Models.User>> SearchAsync(string query, int limit)
    {
        query = query.Trim().ToLower();
        
        return await _context.Users
            .FromSqlRaw(@"
                SELECT * FROM ""user""
                WHERE 
                    username ILIKE '%' || {0} || '%'
                    OR display_name ILIKE '%' || {0} || '%'
                    OR similarity(username, {0}) > 0.3
                    OR similarity(display_name, {0}) > 0.3
                ORDER BY 
                    (
                        CASE 
                            WHEN username ILIKE {0} THEN 1.0
                            WHEN username ILIKE {0} || '%' THEN 0.9
                            WHEN display_name ILIKE {0} || '%' THEN 0.7
                            ELSE 0
                        END
                        +
                        similarity(username, {0}) * 0.6
                        +
                        similarity(display_name, {0}) * 0.4
                    ) DESC
                LIMIT {1}
            ", query, limit)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<bool> FavouriteExists(int userId, int targetUserId)
    {
        return await _context.Favourites
            .AnyAsync(f => f.IdUser == userId && f.IdUserFavourite == targetUserId);
    }

    public async Task AddFavouriteAsync(int userId, int targetUserId)
    {
        var fav = new Favourite
        {
            IdUser = userId,
            IdUserFavourite = targetUserId
        };

        _context.Favourites.Add(fav);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFavouriteAsync(int userId, int targetUserId)
    {
        var fav = await _context.Favourites
            .FirstOrDefaultAsync(f => f.IdUser == userId && f.IdUserFavourite == targetUserId);

        if (fav != null)
        {
            _context.Favourites.Remove(fav);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<List<Models.User>> GetFavouritesAsync(int userId)
    {
        return await _context.Favourites
            .Where(f => f.IdUser == userId)
            .Select(f => f.FavouriteUser)
            .ToListAsync();
    }
    
    public async Task<bool> UserExists(int id)
    {
        return await _context.Users.AnyAsync(u => u.IdUser == id);
    }
}