using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("language")]
public class Language
{
    [Column("id_language")]
    [Key]
    public int IdLanguage { get; set; }
    
    [Column("language_name")]
    [Required]
    [MaxLength(100)]
    public string LanguageName { get; set; } = null!;

    public ICollection<UserLanguage>? UserLanguages { get; set; }
}