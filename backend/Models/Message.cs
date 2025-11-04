using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Message
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Chat))]
    public int ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(Sender))]
    public string SenderUsername { get; set; } = null!;
    public User Sender { get; set; } = null!;

    [MaxLength(1000)]
    public string? Text { get; set; }

    [MaxLength(255)]
    public string? MediaUrl { get; set; }

    [MaxLength(255)]
    public string? PhotoPath { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Notification? Notification { get; set; }
}