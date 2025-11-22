namespace backend.User.DTOs;

public class RegisterUserDTO
{
    public string Username { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateOnly BirthDate { get; set; }
    public string Password { get; set; } = "";
}