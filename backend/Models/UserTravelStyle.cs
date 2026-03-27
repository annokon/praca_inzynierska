using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("user_travel_style")]
public class UserTravelStyle
{
    [Column("id_travel_style")]
    [Required]
    [ForeignKey(nameof(TravelStyle))]
    public int IdTravelStyle { get; set; }
    public TravelStyle TravelStyle { get; set; } = null!;

    [Column("id_user")]
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
}