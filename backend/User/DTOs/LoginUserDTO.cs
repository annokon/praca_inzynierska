namespace backend.User.DTOs;

public class LoginUserDTO
{
    public string Login { get; set; } = "";  // email or username
    public string Password { get; set; } = "";
}
