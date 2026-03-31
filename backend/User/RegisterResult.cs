namespace backend.User;

public class RegisterResult
{
    public bool Success { get; private set; }
    public string? Error { get; private set; }
    public User? User { get; private set; }

    public static RegisterResult Fail(string e) =>
        new RegisterResult
        {
            Success = false,
            Error = e
        };

    public static RegisterResult SuccessResult(User u) =>
        new RegisterResult
        {
            Success = true,
            User = u
        };
}