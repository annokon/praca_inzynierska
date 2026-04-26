using backend.User.DTOs;

namespace backend.User.Services;

public interface IUserService
{
    Task<RegisterResult> RegisterAsync(RegisterUserDTO dto);
    Task<LoginResult> LoginAsync(LoginUserDTO dto);
    
    Task<IEnumerable<UserDTO>> GetAllAsync();
    
    Task<UserDTO?> GetByIdAsync(int idUser, int? currentUserId);
    Task<UserDTO?> GetByUsernameAsync(string username, int? currentUserId);
    
    Task<UserProfileDTO?> GetProfileByIdAsync(int targetUserId, int? currentUserId);
    Task<UserProfileDTO?> GetProfileByUsernameAsync(string username, int? currentUserId);
    
    Task<UserImagesDTO?> GetUserImagesAsync(int userId);
    Task<(bool Success, string? Error)> ResetProfileImageAsync(int? userId);
    Task<(bool Success, string? Error)> ResetBannerImageAsync(int? userId);
    
    Task<List<UserSearchDTO>> SearchAsync(string query, int limit, int? currentUserId);
    
    Task<bool> UpdateAsync(int idUser, UpdateUserDTO dto);
    Task<(bool Success, string? Error, UserDTO? User)> UpdateProfileAsync(int? userId, UpdateUserProfileDTO dto);
    Task<bool> AddAdditionalDataAsync(int idUser, AdditionalDataUserDTO dto);
    
    Task<(bool Success, string? Error, string? ProfilePath, string? BannerPath)> UpdateImagesAsync(int? userId,
        IFormFile? profileImage, IFormFile? bannerImage);
    Task<(bool Success, string? Error)> UpdateCurrencyAsync(int? userId, string currency);
    
    Task<UserDTO> CreateAsync(CreateUserDTO dto);
    Task<bool> DeleteAsync(int idUser);
    
    Task<List<string>> GetAvailableCurrenciesAsync();
}