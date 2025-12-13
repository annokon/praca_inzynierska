namespace backend.User;

public interface IEmailVerificationRepository
{
    Task<Models.User?> GetByEmailAsync(string email);
    Task UpdateAsync(Models.User user);
}