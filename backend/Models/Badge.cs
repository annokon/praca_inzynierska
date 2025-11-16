using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("badge")]
public class Badge
{
    [Column("id_badge")]
    [Key]
    public int IdBadge { get; set; }

    [Column("name_badge")]
    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [Required] 
    [MaxLength(100)]
    public string? Description { get; set; } = null!;

    [Column("icon_path")]
    [Required] 
    [MaxLength(255)]
    public string? IconPath { get; set; } = null!;

    public ICollection<UserBadge>? UserBadges { get; set; }
}