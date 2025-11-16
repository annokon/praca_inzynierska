using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Message
{
    [Key]
    public int IdMessage { get; set; }

    [ForeignKey(nameof(Chat))]
    public int IdChat { get; set; }
    public Chat Chat { get; set; } = null!;

    [ForeignKey(nameof(Sender))]
    public int? IdUserSender { get; set; }
    public User Sender { get; set; } = null!;

    [MaxLength(1000)]
    public string? Text { get; set; }

    [MaxLength(255)]
    public string? MediaUrl { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Notification? Notification { get; set; }
}