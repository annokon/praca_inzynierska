using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserLanguage
{
    [Required]
    [ForeignKey(nameof(Language))]
    public int IdLanguage { get; set; }
    public Language Language { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
}