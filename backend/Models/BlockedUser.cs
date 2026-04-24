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
    
    [InverseProperty(nameof(User.Models.User.UsersBlockingThisUser))]
    public User.Models.User Blocker { get; set; } = null!;
    
    [Column("id_user_blocked")]
    [Required]
    [ForeignKey(nameof(Blocked))]
    public int IdUserBlocked { get; set; }
    
    [InverseProperty(nameof(User.Models.User.BlockedUsers))]
    public User.Models.User Blocked { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}