using backend.Security;

namespace backend.User;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IEmailVerificationRepository _repo;
    private readonly PasswordHasher _hasher;
    private readonly EmailService _email;

    public EmailVerificationService(
        IEmailVerificationRepository repo,
        PasswordHasher hasher,
        EmailService email)
    {
        _repo = repo;
        _hasher = hasher;
        _email = email;
    }

    public async Task<(bool Success, string Message)> SendVerificationAsync(string email)
    {
        var user = await _repo.GetByEmailAsync(email);
        if (user == null)
            return (false, "User not found");

        if (user.EmailVerified)
            return (false, "Email already verified");

        var verificationCode = GenerateVerificationCode();
        
        user.EmailVerificationCodeHash = _hasher.HashPassword(verificationCode);
        user.EmailVerificationExpiresAt = DateTime.UtcNow.AddMinutes(15);
        user.EmailVerificationAttempts = 0;

        await _repo.UpdateAsync(user);
        
        await _email.SendVerificationEmailAsync(email, verificationCode);

        return (true, "Verification code has been sent on email");
    }

    public async Task<(bool Success, string Message)> VerifyAsync(string email, string code)
    {
        var user = await _repo.GetByEmailAsync(email);
        if (user == null)
            return (false, "Incorrect data");

        if (user.EmailVerified)
            return (false, "Email already verified");

        if (user.EmailVerificationExpiresAt < DateTime.UtcNow)
            return (false, "Verification code has expired");

        if (user.EmailVerificationAttempts >= 5)
            return (false, "Too many attempts");

        if (!_hasher.VerifyPassword(code, user.EmailVerificationCodeHash!))
        {
            user.EmailVerificationAttempts++;
            await _repo.UpdateAsync(user);
            return (false, "Incorrect code");
        }

        user.EmailVerified = true;
        user.EmailVerificationCodeHash = null;
        user.EmailVerificationExpiresAt = null;
        user.EmailVerificationAttempts = 0;

        await _repo.UpdateAsync(user);

        return (true, "Email has been verified");
    }

    private string GenerateVerificationCode()
    {
        var random = new Random();
        var verificationCode = random.Next(100000, 999999).ToString();
        return verificationCode;
    }
}