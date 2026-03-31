namespace backend.User.Repositories;

public interface IEmailVerificationRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task UpdateAsync(User user);
}