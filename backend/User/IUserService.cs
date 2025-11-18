using backend.DTOs;
using backend.DTOs.UserDTOs;

namespace backend.User;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<UserDTO?> GetByIdUserAsync(int idUser);
    Task<UserDTO> CreateAsync(CreateUserDTO dto);
    Task<bool> UpdateAsync(int idUser, UpdateUserDTO dto);
    Task<bool> AddAdditionalDataAsync(int idUser, AdditionalDataUserDTO dto);
    Task<bool> DeleteAsync(int idUser);
}