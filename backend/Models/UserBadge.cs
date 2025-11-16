using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserBadge
{
    [ForeignKey(nameof(User))]
    public int? IdUser { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Badge))]
    public int IdBadge { get; set; }
    public Badge Badge { get; set; } = null!;
}