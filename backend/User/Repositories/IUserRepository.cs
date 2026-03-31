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
}