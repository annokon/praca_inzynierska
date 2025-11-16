using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Language
{
    [Key]
    public int IdLanguage { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LanguageName { get; set; } = null!;

    public ICollection<UserLanguage>? UserLanguages { get; set; }
}