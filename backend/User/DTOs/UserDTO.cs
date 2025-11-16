namespace backend.DTOs.UserDTOs;

public class UserDTO
{
    // required data
    public string Username { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string Gender { get; set; } = null!;

    // optional data
    public string? Pronouns { get; set; }
    public string? Location { get; set; }
    public string? PersonalityType { get; set; }
    public string? AlcoholPreference { get; set; }
    public string? SmokingPreference { get; set; }
    public string? DrivingLicenseType { get; set; }
    public string? TravelStyle { get; set; }
    public string? TravelExperience { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePhotoPath { get; set; }
    public string? BackgroundPhotoPath { get; set; }

    // system data
    public bool EmailVerified { get; set; }
    public bool IsActive { get; set; }
    public string Role { get; set; } = "user";
}