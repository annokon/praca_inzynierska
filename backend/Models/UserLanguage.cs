using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("user_language")]
public class UserLanguage
{
    [Column("id_language")]
    [Required]
    [ForeignKey(nameof(Language))]
    public int IdLanguage { get; set; }
    public Language Language { get; set; } = null!;

    [Column("id_user")]
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
}