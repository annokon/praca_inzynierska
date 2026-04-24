using backend.Models;

namespace backend.User.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<Models.User>> GetAllAsync();
    Task<Models.User?> GetByIdUserAsync(int idUser);
    Task AddAsync(Models.User user);
    Task UpdateAsync(Models.User user);
    Task DeleteAsync(Models.User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<Models.User?> GetUserWithLanguagesAsync(int idUser);
    Task<Models.User?> GetByEmailOrUsernameAsync(string login);
    Task<bool> ValidateGender(int dtoGenderId);
    Task<Models.User?> GetUserWithRelationsAsync(int id);
    Task SaveChangesAsync();
    Task<bool> ValidatePersonalityType(int personalityId);
    Task<bool> ValidateAlcoholPreference(int alcoholId);
    Task<bool> ValidateSmokingPreference(int smokingId);
    Task<bool> ValidatePronouns(int pronounsId);
    Task<bool> ValidateDrivingLicense(int drivingLicenseId);
    Task<bool> ValidateTravelExperience(int travelExperienceId);
    Task<bool> ValidateInterest(int id);
    Task<bool> ValidateTravelStyle(int id);
    Task<bool> ValidateTransportMode(int id);
    Task<bool> ValidateLanguages(List<int> ids);
    Task<bool> UpdateCurrencyAsync(int userId, string currency);
    Task<List<Models.User>> SearchAsync(string query, int limit);
    Task<bool> UserExists(int id);
}