using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.User.Models;

[Table("blocked_users")]
public class BlockedUser
{
    [Column("id_blocked")]
    [Key]
    public int IdBlocked { get; set; }
    
    [Column("id_user_blocker")]
    [ForeignKey(nameof(Blocker))]
    public int IdUserBlocker { get; set; }
    
    [InverseProperty(nameof(User.UsersBlockingThisUser))]
    public User Blocker { get; set; } = null!;
    
    [Column("id_user_blocked")]
    [ForeignKey(nameof(Blocked))]
    public int IdUserBlocked { get; set; }
    
    [InverseProperty(nameof(User.BlockedUsers))]
    public User Blocked { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}