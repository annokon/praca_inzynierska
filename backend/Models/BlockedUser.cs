using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("blocked_users")]
public class BlockedUser
{
    [Column("id_user_blocker")]
    [Required]
    [ForeignKey(nameof(Blocker))]
    public int IdUserBlocker { get; set; }
    
    [InverseProperty(nameof(User.User.UsersBlockingThisUser))]
    public User.User Blocker { get; set; } = null!;
    
    [Column("id_user_blocked")]
    [Required]
    [ForeignKey(nameof(Blocked))]
    public int IdUserBlocked { get; set; }
    
    [InverseProperty(nameof(User.User.BlockedUsers))]
    public User.User Blocked { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}