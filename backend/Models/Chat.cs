using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Chat
{
    [Key]
    public int Id { get; set; }

    public bool IsGroupChat { get; set; } = false;

    [ForeignKey(nameof(Trip))]
    public int? TripId { get; set; }
    public Trip? Trip { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserChat>? UserChats { get; set; }
    public ICollection<Message>? Messages { get; set; }
}