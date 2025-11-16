using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class SearchFilter
{
    [Key]
    public int IdSearchFilter { get; set; }

    [ForeignKey(nameof(User))]
    public int? IdUser { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(1000)]
    public string Criteria { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}