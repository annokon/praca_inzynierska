using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserBadge
{
    [MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string UserUsername { get; set; } = null!;
    public User User { get; set; } = null!;

    [ForeignKey(nameof(Badge))]
    public int BadgeBadgeId { get; set; }
    public Badge Badge { get; set; } = null!;
}