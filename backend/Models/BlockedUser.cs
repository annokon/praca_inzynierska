using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class BlockedUser
{
    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(Blocker))]
    public string BlockerUsername { get; set; } = null!;
    public User Blocker { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(Blocked))]
    public string BlockedUsername { get; set; } = null!;
    public User Blocked { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}