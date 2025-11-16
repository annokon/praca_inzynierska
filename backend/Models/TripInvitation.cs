using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("trip_invitation")]
public class TripInvitation
{
    [Column("id_trip_invitation")]
    [Key]
    public int IdTripInvitation { get; set; }      

    [Column("id_trip")]
    [Required]
    [ForeignKey(nameof(Trip))]
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    // user inviting
    [Column("id_user_inviting")]
    [ForeignKey(nameof(InvitingUser))]
    public int? IdUserInviting { get; set; }
    
    [InverseProperty(nameof(User.TripInvitationsSent))]
    public User InvitingUser { get; set; } = null!;

    // user invited
    [Column("id_user_invited")]
    [ForeignKey(nameof(InvitedUser))]
    public int? IdUserInvited { get; set; }
    [InverseProperty(nameof(User.TripInvitationsReceived))]
    public User InvitedUser { get; set; } = null!;

    [Column("status")]
    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "pending";

    [Column("sent_at")]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public Notification? Notification { get; set; }

}