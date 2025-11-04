using backend.DTOs;
using backend.DTOs.UserDTOs;

namespace backend.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<UserDTO?> GetByUsernameAsync(string username);
    Task<UserDTO> CreateAsync(CreateUserDTO dto);
    Task<bool> UpdateAsync(string username, UpdateUserDTO dto);
    Task<bool> AddAdditionalDataAsync(string username, AdditionalDataUserDTO dto);
    Task<bool> DeleteAsync(string username);
}