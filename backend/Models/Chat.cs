using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace backend.Models;

[Table("chat")]
public class Chat
{
    [Column("id_chat")]
    [Key]
    public int IdChat { get; set; }

    [Column("is_group_chat")]
    public bool IsGroupChat { get; set; } = false;

    [Column("id_trip")]
    [ForeignKey(nameof(Trip))]
    public int? IdTrip { get; set; }
    public Trip? Trip { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserChat>? UserChats { get; set; }
    public ICollection<Message>? Messages { get; set; }
}