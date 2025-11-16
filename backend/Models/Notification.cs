using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("notification")]
public class Notification
{
    [Column("id_notification")]
    [Key]
    public int IdNotification { get; set; }

    //user that got notification
    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public int? IdUser { get; set; }
    public User User { get; set; } = null!;

    [Column("id_message")]
    public int? IdMessage { get; set; }      
    public Message? Message { get; set; }

    [Column("id_trip_invitation")]
    public int? IdTripInvitation { get; set; }
    public TripInvitation? TripInvitation { get; set; }

    [Column("id_review")]
    public int? IdReview { get; set; }
    public Review? Review { get; set; }

    [Column("type")]
    [MaxLength(20)]
    public string? Type { get; set; } = "message";

    [Column("is_read")]
    public bool IsRead { get; set; } = false;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}