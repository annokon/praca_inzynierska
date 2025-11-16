using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class TripInvitation
{
    [Key]
    public int IdTripInvitation { get; set; }      

    [Required]
    [ForeignKey(nameof(Trip))]
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    // user inviting
    [ForeignKey(nameof(InvitingUser))]
    public int? IdUserInviting { get; set; }
    
    [InverseProperty(nameof(User.TripInvitationsSent))]
    public User InvitingUser { get; set; } = null!;

    // user invited
    [ForeignKey(nameof(InvitedUser))]
    public int? IdUserInvited { get; set; }
    [InverseProperty(nameof(User.TripInvitationsReceived))]
    public User InvitedUser { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "pending";

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public Notification? Notification { get; set; }

}