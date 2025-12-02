using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("language")]
public class Language
{
    [Column("id_language")]
    [Key]
    public int IdLanguage { get; set; }
    
    [Column("language_name_pl")]
    [Required]
    [MaxLength(100)]
    public string LanguageNamePL { get; set; } = null!;
    
    [Column("language_name_en")]
    [Required]
    [MaxLength(100)]
    public string LanguageNameEN { get; set; } = null!;
    
    public ICollection<UserLanguage>? UserLanguages { get; set; }
}