using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserChat
{
    [ForeignKey(nameof(Chat))]
    public int ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    [MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string UserUsername { get; set; } = null!;
    public User User { get; set; } = null!;
}