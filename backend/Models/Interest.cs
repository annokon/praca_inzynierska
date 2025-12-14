using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("interest")]
public class Interest
{
    [Column("id_interest")]
    [Key]
    public int IdInterest { get; set; }

    [Column("interest_name")]
    [Required]
    [MaxLength(100)]
    public string InterestName { get; set; } = null!;

    public ICollection<UserInterest>? UserInterests { get; set; }
}