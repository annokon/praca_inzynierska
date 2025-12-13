namespace backend.User.DTOs;

public class VerifyEmailDTO
{
    public string Email { get; set; } = "";
    public string Code { get; set; } = "";
}