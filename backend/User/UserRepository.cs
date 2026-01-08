using backend.Infrastructure.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.User;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    
    // get all users
    Task<IEnumerable<Models.User>> IUserRepository.GetAllAsync()
    {
        return GetAllAsync();
    }
    
    public async Task<IEnumerable<Models.User>> GetAllAsync() =>
        await _context.Users.AsNoTracking().ToListAsync();

    
    // get user by id
    Task<Models.User?> IUserRepository.GetByIdUserAsync(int idUser)
    {
        return GetByIdUserAsync(idUser);
    }
    
    public async Task<Models.User?> GetByIdUserAsync(int idUser) =>
        await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);

    
    // add new user
    Task IUserRepository.AddAsync(Models.User user)
    {
        return AddAsync(user);
    }
    
    public async Task AddAsync(Models.User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    
    // update user
    Task IUserRepository.UpdateAsync(Models.User user)
    {
        return UpdateAsync(user);
    }

    public async Task UpdateAsync(Models.User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    
    
    // delete user
    Task IUserRepository.DeleteAsync(Models.User user)
    {
        return DeleteAsync(user);
    }
    
    public async Task DeleteAsync(Models.User user)
    {
        //TODO
    }

    
    // does any user with this email already exist?
    public async Task<bool> ExistsByEmailAsync(string email) =>
        await _context.Users.AnyAsync(u => u.Email == email);

    // does any user with this username already exist?
    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _context.Users.AnyAsync(u => u.Username == username);

    
    public async Task<Models.User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    
    public async Task<Models.User?> GetUserWithLanguagesAsync(int idUser)
    {
        return await _context.Users
            .Include(u => u.UserLanguages)!
            .ThenInclude(ul => ul.Language)
            .FirstOrDefaultAsync(u => u.IdUser == idUser);
    }
}