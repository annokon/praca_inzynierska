using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Notification
{
    [Key]
    public int IdNotification { get; set; }

    //user that got notification
    [ForeignKey(nameof(User))]
    public int? IdUser { get; set; }
    public User User { get; set; } = null!;

    public int? IdMessage { get; set; }      
    public Message? Message { get; set; }

    public int? IdTripInvitation { get; set; }
    public TripInvitation? TripInvitation { get; set; }

    public int? IdReview { get; set; }
    public Review? Review { get; set; }

    [MaxLength(20)]
    public string? Type { get; set; } = "message";

    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}