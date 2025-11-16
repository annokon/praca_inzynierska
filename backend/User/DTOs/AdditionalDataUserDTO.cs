using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class AdditionalDataUserDTO
{
    // optional data
    [MaxLength(20)]
    public string? Pronouns { get; set; }

    [MaxLength(100)]
    public string? Location { get; set; }

    [MaxLength(20)]
    public string? PersonalityType { get; set; }

    [MaxLength(50)]
    public string? AlcoholPreference { get; set; }

    [MaxLength(50)]
    public string? SmokingPreference { get; set; }

    [MaxLength(20)]
    public string? DrivingLicenseType { get; set; }

    [MaxLength(50)]
    public string? TravelStyle { get; set; }

    [MaxLength(50)]
    public string? TravelExperience { get; set; }
}