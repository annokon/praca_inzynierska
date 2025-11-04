using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Badge
{
    [Key]
    public int BadgeId { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? Description { get; set; }

    [MaxLength(255)]
    public string? IconPath { get; set; }

    public ICollection<UserBadge>? UserBadges { get; set; }
}