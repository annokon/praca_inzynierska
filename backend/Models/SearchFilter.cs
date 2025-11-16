using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("search_filter")]
public class SearchFilter
{
    [Column("id_search_filter")]
    [Key]
    public int IdSearchFilter { get; set; }

    [Column("id_user")]
    [ForeignKey(nameof(User))]
    public int? IdUser { get; set; }
    public User User { get; set; } = null!;

    [Column("name_search_filter")]
    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = null!;

    [Column("criteria")]
    [Required]
    [MaxLength(1000)]
    public string Criteria { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}