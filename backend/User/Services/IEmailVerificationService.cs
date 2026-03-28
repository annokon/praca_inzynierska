namespace backend.User;

public interface IEmailVerificationService
{
    Task<(bool Success, string Message)> VerifyAsync(string email, string code);
    Task<(bool Success, string Message)> SendVerificationAsync(string email);
}