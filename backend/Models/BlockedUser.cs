using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class BlockedUser
{
    [Required]
    [ForeignKey(nameof(Blocker))]
    public int IdUserBlocker { get; set; }
    
    [InverseProperty(nameof(User.UsersBlockingThisUser))]
    public User Blocker { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Blocked))]
    public int IdUserBlocked { get; set; }
    
    [InverseProperty(nameof(User.BlockedUsers))]
    public User Blocked { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}