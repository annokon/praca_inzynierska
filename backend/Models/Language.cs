using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Language
{
    [Key]
    [MaxLength(20)]
    public string Name { get; set; } = null!;

    public ICollection<UserLanguage>? UserLanguages { get; set; }
}