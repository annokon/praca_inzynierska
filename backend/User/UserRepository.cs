using backend.Infrastructure.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    
    // get all users
    Task<IEnumerable<User>> IUserRepository.GetAllAsync()
    {
        return GetAllAsync();
    }
    
    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _context.Users.AsNoTracking().ToListAsync();

    
    // get user by id
    Task<User?> IUserRepository.GetByIdUserAsync(int idUser)
    {
        return GetByIdUserAsync(idUser);
    }
    
    public async Task<User?> GetByIdUserAsync(int idUser) =>
        await _context.Users.FirstOrDefaultAsync(u => u.IdUser == idUser);

    
    // add new user
    Task IUserRepository.AddAsync(User user)
    {
        return AddAsync(user);
    }
    
    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    
    // update user
    Task IUserRepository.UpdateAsync(User user)
    {
        return UpdateAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    
    
    // delete user
    Task IUserRepository.DeleteAsync(User user)
    {
        return DeleteAsync(user);
    }
    
    public async Task DeleteAsync(User user)
    {
        //TODO
    }

    
    // does any user with this email already exist?
    public async Task<bool> ExistsByEmailAsync(string email) =>
        await _context.Users.AnyAsync(u => u.Email == email);

    // does any user with this username already exist?
    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _context.Users.AnyAsync(u => u.Username == username);
}