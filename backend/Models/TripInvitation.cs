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
    [Required, MaxLength(20)]
    [ForeignKey(nameof(InvitingUser))]
    public string IdUserInviting { get; set; } = null!;
    public User InvitingUser { get; set; } = null!;

    // user invited
    [Required, MaxLength(20)]
    [ForeignKey(nameof(InvitedUser))]
    public string IdUserInvited { get; set; } = null!;
    public User InvitedUser { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "pending";

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public Notification? Notification { get; set; }

}