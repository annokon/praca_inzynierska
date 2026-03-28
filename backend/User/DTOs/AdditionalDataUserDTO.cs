using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class AdditionalDataUserDTO
{
    public int? GenderId { get; set; }

    public int? PronounsId { get; set; }

    [MaxLength(100)]
    public string? Location { get; set; }

    public int? PersonalityTypeId { get; set; }

    public int? AlcoholPreferenceId { get; set; }

    public int? SmokingPreferenceId { get; set; }

    public int? DrivingLicenseTypeId { get; set; }

    public int? TravelExperienceId { get; set; }

    public List<int> LanguageIds { get; set; } = new();

    public List<int> InterestIds { get; set; } = new();

    public List<int> TravelStyleIds { get; set; } = new();

    public List<int> TransportModeIds { get; set; } = new();
}