using backend.Models;

namespace backend.User;

public interface IUserRepository
{
    Task<IEnumerable<Models.User>> GetAllAsync();
    Task<Models.User?> GetByIdUserAsync(int idUser);
    Task AddAsync(Models.User user);
    Task UpdateAsync(Models.User user);
    Task DeleteAsync(Models.User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<Models.User?> GetByEmailAsync(string email);
}