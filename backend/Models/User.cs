using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    [Key] 
    [MaxLength(20)] 
    public string Username { get; set; } = null!;

    [MaxLength(30)] 
    public string DisplayName { get; set; } = null!;

    [MaxLength(320)] 
    public string Email { get; set; } = null!;

    [MaxLength(255)] 
    public string PasswordHash { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    [MaxLength(20)] 
    public string? Gender { get; set; }

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

    [MaxLength(500)] 
    public string? Bio { get; set; }

    [MaxLength(255)] 
    public string? ProfilePhotoPath { get; set; }

    [MaxLength(255)] 
    public string? BackgroundPhotoPath { get; set; }

    public bool IsActive { get; set; } = true;

    public bool EmailVerified { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(20)] 
    public string Role { get; set; } = "user";

    public bool ToBeDeleted { get; set; } = false;
    public DateTime? DeleteDate { get; set; }

    public bool IsBlocked { get; set; } = false;
    
    //for Review
    public ICollection<Review>? ReviewsWritten { get; set; }
    public ICollection<Review>? ReviewsReceived { get; set; }
    
    //for SearchFilter
    public ICollection<SearchFilter>? SearchFilters { get; set; }
    
    //for BlockedUser
    public ICollection<BlockedUser>? BlockedUsers { get; set; } 
    public ICollection<BlockedUser>? UsersBlockingThisUser { get; set; } 

}