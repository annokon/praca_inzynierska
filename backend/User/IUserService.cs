using backend.DTOs;
using backend.DTOs.UserDTOs;
using backend.User.DTOs;

namespace backend.User;

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
}