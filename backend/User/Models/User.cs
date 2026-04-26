using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.CategoriesOptions.Models;
using backend.Interest;
using backend.Language;
using backend.Models;
using backend.TransportMode;
using backend.TravelStyle;

namespace backend.User.Models;

[Table("user")]
public class User
{
    [Column("id_user")]
    [Key] 
    public int IdUser { get; set; }
    
    [Column("username")]
    [Required]
    [MaxLength(100)] 
    public string Username { get; set; } = null!;

    [Column("display_name")]
    [Required]
    [MaxLength(30)] 
    public string DisplayName { get; set; } = null!;

    [Column("email")]
    [Required]
    [MaxLength(320)] 
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    [Required]
    [MaxLength(255)] 
    public string PasswordHash { get; set; } = null!;

    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }

    [Column("gender")]
    public int? GenderId { get; set; }
    public GenderOption? Gender { get; set; }

    [Column("pronouns")]
    public int? PronounsId { get; set; }
    public PronounOption? Pronouns { get; set; }

    [Column("location")]
    [MaxLength(100)] 
    public string? Location { get; set; }

    [Column("personality_type")]
    public int? PersonalityTypeId { get; set; }
    public PersonalityTypeOption? PersonalityType { get; set; }

    [Column("alcohol_preference")]
    public int? AlcoholPreferenceId { get; set; }
    public AlcoholPreferenceOption? AlcoholPreference { get; set; }

    [Column("smoking_preference")]
    public int? SmokingPreferenceId { get; set; }
    public SmokingPreferenceOption? SmokingPreference { get; set; }

    [Column("driving_license_type")]
    public int? DrivingLicenseId { get; set; }
    public DrivingLicenseOption? DrivingLicense { get; set; }

    [Column("travel_experience")]
    public int? TravelExperienceId { get; set; }
    public TravelExperienceOption? TravelExperience { get; set; }

    [Column("bio")]
    [MaxLength(500)] 
    public string? Bio { get; set; }

    [Column("profile_photo_path")]
    [MaxLength(255)] 
    public string? ProfilePhotoPath { get; set; }

    [Column("background_photo_path")]
    [MaxLength(255)] 
    public string? BackgroundPhotoPath { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("email_verified")]
    public bool EmailVerified { get; set; } = false;
    
    [Column("email_verification_code_hash")]
    [MaxLength(255)]
    public string? EmailVerificationCodeHash { get; set; }

    [Column("email_verification_expires_at")]
    public DateTime? EmailVerificationExpiresAt { get; set; }

    [Column("email_verification_attempts")]
    public int EmailVerificationAttempts { get; set; } = 0;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("role")]
    [Required]
    [MaxLength(20)] 
    public string Role { get; set; } = "user";

    [Column("to_be_deleted")]
    public bool ToBeDeleted { get; set; } = false;
    
    [Column("delete_date")]
    public DateTime? DeleteDate { get; set; }

    [Column("is_blocked")]
    public bool IsBlocked { get; set; } = false;

    [Column("currency")]
    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = null!;
    
    //for Review
    [InverseProperty(nameof(Review.Reviewer))]
    public ICollection<Review>? ReviewsWritten { get; set; }
    
    [InverseProperty(nameof(Review.ReviewedUser))]
    public ICollection<Review>? ReviewsReceived { get; set; }
    
    //for SearchFilter
    public ICollection<SearchFilter>? SearchFilters { get; set; }
    
    //for BlockedUser
    [InverseProperty(nameof(BlockedUser.Blocked))]
    public ICollection<BlockedUser>? BlockedUsers { get; set; } 
    [InverseProperty(nameof(BlockedUser.Blocker))]
    public ICollection<BlockedUser>? UsersBlockingThisUser { get; set; } 

    //for UserLanguage 
    public ICollection<UserLanguage>? UserLanguages { get; set; }
    
    //for Favourite
    [InverseProperty(nameof(Favourite.User))]
    public ICollection<Favourite>? FavouritesGiven { get; set; }         // user added others
    [InverseProperty(nameof(Favourite.FavouriteUser))]
    public ICollection<Favourite>? FavouritesReceived { get; set; }      // others added user
    
    //for UserBadge
    public ICollection<UserBadge>? UserBadges { get; set; }
    
    //for TripPhoto
    public ICollection<TripPhoto>? TripPhotosUploaded { get; set; }
    
    //for TripInvitation
    [InverseProperty(nameof(TripInvitation.InvitingUser))]
    public ICollection<TripInvitation>? TripInvitationsSent { get; set; }
    [InverseProperty(nameof(TripInvitation.InvitedUser))]
    public ICollection<TripInvitation>? TripInvitationsReceived { get; set; }

    //for UserChat
    public ICollection<UserChat>? UserChats { get; set; }
    
    //for Notification
    public ICollection<Notification.Notification>? Notifications { get; set; }
    
    //for interest
    public ICollection<UserInterest>? UserInterests { get; set; }

    //for transport mode
    public ICollection<UserTransportMode>? UserTransportModes { get; set; }
    
    // for travel style
    public ICollection<UserTravelStyle>? UserTravelStyles { get; set; }

}