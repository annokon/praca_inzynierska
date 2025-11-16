using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("user_chat")]
public class UserChat
{
    [Column("id_chat")]
    [Required]
    [ForeignKey(nameof(Chat))]
    public int IdChat { get; set; }
    public Chat Chat { get; set; } = null!;
    
    [Column("id_user")]
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
}