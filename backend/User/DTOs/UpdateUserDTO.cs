using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class UpdateUserDTO
{
    // required data
    [MaxLength(20)]
    public int? IdUser { get; set; }
    
    [MaxLength(20)]
    public string? Username { get; set; }
    
    [MaxLength(30)]
    public string? DisplayName { get; set; }

    [EmailAddress, MaxLength(320)]
    public string? Email { get; set; }
    
    [MaxLength(20)]
    public string? Gender { get; set; }
    
    
    // optional data
    [MaxLength(100)] 
    public string? Location { get; set; }
    
    [MaxLength(20)]
    public string? Pronouns { get; set; }

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

    [MaxLength(500)]
    public string? Bio { get; set; }

    [MaxLength(255)]
    public string? ProfilePhotoPath { get; set; }

    [MaxLength(255)]
    public string? BackgroundPhotoPath { get; set; }
    
    
    // system data
    public bool? IsActive { get; set; }

    public bool? IsBlocked { get; set; }

    [MaxLength(20)]
    public string? Role { get; set; }
}