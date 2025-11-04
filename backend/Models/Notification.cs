using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Notification
{
    [Key]
    public int Id { get; set; }

    //user that got notification
    [Required, MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string UserUsername { get; set; } = null!;
    public User User { get; set; } = null!;

    public int? MessageId { get; set; }      
    public Message? Message { get; set; }

    public int? TripInvitationId { get; set; }
    public TripInvitation? TripInvitation { get; set; }

    public int? ReviewId { get; set; }
    public Review? Review { get; set; }

    [MaxLength(20)]
    public string? Type { get; set; } = "message";

    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}