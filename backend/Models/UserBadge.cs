using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("user_badge")]
public class UserBadge
{
    [Column("id_user")]
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;

    [Column("id_badge")]
    [Required]
    [ForeignKey(nameof(Badge))]
    public int IdBadge { get; set; }
    public Badge Badge { get; set; } = null!;
}