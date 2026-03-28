using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.User;

public class EmailVerificationRepository : IEmailVerificationRepository
{
    private readonly DataContext _context;

    public EmailVerificationRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Models.User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task UpdateAsync(Models.User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}