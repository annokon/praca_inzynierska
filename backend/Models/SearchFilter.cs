using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class SearchFilter
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string UserUsername { get; set; } = null!;
    public User User { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    public string Criteria { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}