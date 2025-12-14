using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("user_interest")]
public class UserInterest
{
    [Column("id_interest")]
    [Required]
    [ForeignKey(nameof(Interest))]
    public int IdInterest { get; set; }
    public Interest Interest { get; set; } = null!;

    [Column("id_user")]
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
}