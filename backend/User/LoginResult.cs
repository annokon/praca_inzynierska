namespace backend.User;

public class LoginResult
{
    public bool Success { get; private set; }
    public string? Error { get; private set; }
    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public string? Role { get; private set; }

    public static LoginResult Fail(string e) =>
        new LoginResult
        {
            Success = false,
            Error = e
        };

    public static LoginResult SuccessResult(string access, string refresh, string role) =>
        new LoginResult
        {
            Success = true,
            AccessToken = access,
            RefreshToken = refresh,
            Role = role
        };
}