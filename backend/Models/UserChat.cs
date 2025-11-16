using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserChat
{
    [Required]
    [ForeignKey(nameof(Chat))]
    public int IdChat { get; set; }
    public Chat Chat { get; set; } = null!;
    
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
}