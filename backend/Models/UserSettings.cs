using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserSettings
{
    //to User
    [Key]
    [MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string UserUsername { get; set; } = null!;
    public User User { get; set; } = null!;

    // to Language
    [MaxLength(20)]
    [ForeignKey(nameof(Language))]
    public string? LanguageName { get; set; }
    public Language? Language { get; set; }

    [MaxLength(3)]
    public string? Currency { get; set; }

}