using backend.User.DTOs;

namespace backend.User.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<UserDTO?> GetByIdUserAsync(int idUser);
    Task<UserDTO> CreateAsync(CreateUserDTO dto);
    Task<bool> UpdateAsync(int idUser, UpdateUserDTO dto);
    Task<bool> AddAdditionalDataAsync(int idUser, AdditionalDataUserDTO dto);
    Task<bool> DeleteAsync(int idUser);
    Task<RegisterResult> RegisterAsync(RegisterUserDTO dto);
    Task<LoginResult> LoginAsync(LoginUserDTO dto);
    Task<UserDTO?> GetByIdAsync(int id);
    Task<UserProfileDTO?> GetProfileAsync(int id);
    Task<(bool Success, string? Error, UserDTO? User)> UpdateDisplayNameAsync(int userId, string displayName);
    Task<(bool Success, string? Error, UserDTO? User)> UpdateUsernameAsync(int userId, string username);
    Task<(bool Success, string? Error, UserDTO? User)> UpdateBirthDateAsync(int userId, DateOnly birthDate);
    Task<(bool Success, string? Error, UserDTO? User)> UpdateGenderAsync(int userId, int? genderId);
    Task<(bool Success, string? Error)> UpdateLanguagesAsync(int userId, List<int> languageIds);

}