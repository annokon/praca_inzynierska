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
    Task<(bool Success, string? Error, UserDTO? User)> UpdateProfileAsync(int userId, UpdateUserProfileDTO dto);
    Task<(bool Success, string? Error, string? ProfilePath, string? BannerPath)>
        UpdateImagesAsync(int userId, IFormFile? profileImage, IFormFile? bannerImage);

    Task<UserImagesDTO?> GetUserImagesAsync(int userId);
    Task<(bool Success, string? Error)> UpdateCurrencyAsync(int userId, string currency);
    Task<List<string>> GetAvailableCurrenciesAsync();
    Task<List<UserSearchDTO>> SearchAsync(string query, int limit);
}