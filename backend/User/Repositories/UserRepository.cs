using backend.Infrastructure.Data;
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
    Task<IEnumerable<User>> IUserRepository.GetAllAsync()
    {
        return GetAllAsync();
    }
    
    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _context.Users.AsNoTracking().ToListAsync();

    
    // get user by id
    Task<User?> IUserRepository.GetByIdUserAsync(int idUser)
    {
        return GetByIdUserAsync(idUser);
    }
    
    public async Task<User?> GetByIdUserAsync(int idUser) =>
        await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);

    
    // add new user
    Task IUserRepository.AddAsync(User user)
    {
        return AddAsync(user);
    }
    
    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    
    // update user
    Task IUserRepository.UpdateAsync(User user)
    {
        return UpdateAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    
    
    // delete user
    Task IUserRepository.DeleteAsync(User user)
    {
        return DeleteAsync(user);
    }
    
    public async Task DeleteAsync(User user)
    {
        //TODO
    }

    
    // does any user with this email already exist?
    public async Task<bool> ExistsByEmailAsync(string email) =>
        await _context.Users.AnyAsync(u => u.Email == email);

    // does any user with this username already exist?
    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _context.Users.AnyAsync(u => u.Username == username);

    
    public async Task<User?> GetByEmailOrUsernameAsync(string login)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == login || u.Username == login);
    }
    
    public async Task<User?> GetUserWithLanguagesAsync(int idUser)
    {
        return await _context.Users
            .Include(u => u.UserLanguages)!
            .ThenInclude(ul => ul.Language)
            .FirstOrDefaultAsync(u => u.IdUser == idUser);
    }

    public async Task<bool> ValidateGender(int dtoGenderId) =>
        await _context.GenderOptions.AnyAsync(g => g.IdGender == dtoGenderId);
    
    public async Task<User?> GetUserWithRelationsAsync(int id)
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

}