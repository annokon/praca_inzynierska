using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserLanguage
{
    [MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string UserUsername { get; set; } = null!;
    public User User { get; set; } = null!;

    [MaxLength(20)]
    [ForeignKey(nameof(Language))]
    public string LanguageName { get; set; } = null!;
    public Language Language { get; set; } = null!;
}