using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class BlockedUser
{
    [ForeignKey(nameof(Blocker))]
    public int? IdUserBlocker { get; set; }
    public User Blocker { get; set; } = null!;

    [ForeignKey(nameof(Blocked))]
    public int? IdUserBlocked { get; set; }
    public User Blocked { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}