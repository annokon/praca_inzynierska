using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class User
{
    [Key] 
    public int IdUser { get; set; }
    
    [Required]
    [MaxLength(100)] 
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(30)] 
    public string DisplayName { get; set; } = null!;

    [Required]
    [MaxLength(320)] 
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(255)] 
    public string PasswordHash { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    [Required]
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

    [Required]
    [MaxLength(20)] 
    public string Role { get; set; } = "user";

    public bool ToBeDeleted { get; set; } = false;
    public DateTime? DeleteDate { get; set; }

    public bool IsBlocked { get; set; } = false;
    
    [Required]
    [MaxLength(3)]
    public string? Currency { get; set; }

    [Required]
    [MaxLength(100)]
    public string? SystemLanguage { get; set; }
    
    //for Review
    [InverseProperty(nameof(Review.Reviewer))]
    public ICollection<Review>? ReviewsWritten { get; set; }
    
    [InverseProperty(nameof(Review.ReviewedUser))]
    public ICollection<Review>? ReviewsReceived { get; set; }
    
    //for SearchFilter
    public ICollection<SearchFilter>? SearchFilters { get; set; }
    
    //for BlockedUser
    public ICollection<BlockedUser>? BlockedUsers { get; set; } 
    public ICollection<BlockedUser>? UsersBlockingThisUser { get; set; } 

    //for UserLanguage 
    public ICollection<UserLanguage>? UserLanguages { get; set; }
    
    //for Favourite
    public ICollection<Favourite>? FavouritesGiven { get; set; }         // user added others
    public ICollection<Favourite>? FavouritesReceived { get; set; }      // others added user
    
    //for UserBadge
    public ICollection<UserBadge>? UserBadges { get; set; }
    
    //for TripPhoto
    public ICollection<TripPhoto>? TripPhotosUploaded { get; set; }
    
    //for TripInvitation
    public ICollection<TripInvitation>? TripInvitationsSent { get; set; }
    public ICollection<TripInvitation>? TripInvitationsReceived { get; set; }

    //for UserChat
    public ICollection<UserChat>? UserChats { get; set; }

}