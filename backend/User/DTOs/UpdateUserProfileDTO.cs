namespace backend.User.DTOs;

public class UpdateUserProfileDTO
{
    public string? DisplayName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public DateOnly? BirthDate { get; set; }

    public int? GenderId { get; set; }
    public int? PronounsId { get; set; }
    public int? PersonalityTypeId { get; set; }

    public string? Bio { get; set; }
    public string? Location { get; set; }

    public int? AlcoholPreferenceId { get; set; }
    public int? SmokingPreferenceId { get; set; }
    public int? DrivingLicenseId { get; set; }
    public int? TravelExperienceId { get; set; }

    public List<int>? LanguageIds { get; set; }
    public List<int>? InterestIds { get; set; }
    public List<int>? TravelStyleIds { get; set; }
    public List<int>? TransportModeIds { get; set; }
}