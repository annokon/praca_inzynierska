namespace backend.User.DTOs;

public class UserProfileDTO
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Email { get; set; } = "";
    public string BirthDate { get; set; }

    public string? Gender { get; set; }
    public string? Pronouns { get; set; }
    public string? Personality { get; set; }
    public string? Location { get; set; }
    public string? Bio { get; set; }

    public List<string> Languages { get; set; } = [];
}