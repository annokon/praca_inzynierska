using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("message")]
public class Message
{
    [Column("id_message")]
    [Key]
    public int IdMessage { get; set; }

    [Column("id_chat")]
    [Required]
    public int IdUser { get; set; }
    [ForeignKey(nameof(Chat))]
    public int IdChat { get; set; }
    public Chat Chat { get; set; } = null!;

    [Column("id_user_sender")]
    [ForeignKey(nameof(Sender))]
    public int? IdUserSender { get; set; }
    public User Sender { get; set; } = null!;

    [Column("text")]
    [MaxLength(1000)]
    public string? Text { get; set; }

    [Column("media_url")]
    [MaxLength(255)]
    public string? MediaUrl { get; set; }

    [Column("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public ICollection<Notification>? Notifications { get; set; }
}