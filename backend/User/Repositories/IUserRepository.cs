using backend.Models;

namespace backend.User.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdUserAsync(int idUser);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<User?> GetUserWithLanguagesAsync(int idUser);
    Task<User?> GetByEmailOrUsernameAsync(string login);
    Task<bool> ValidateGender(int dtoGenderId);
    Task<User?> GetUserWithRelationsAsync(int id);
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

}