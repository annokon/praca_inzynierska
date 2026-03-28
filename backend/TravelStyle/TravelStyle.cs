using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace backend.TravelStyle;

[Table("travel_style")]
public class TravelStyle
{
    [Column("id_travel_style")]
    [Key]
    public int IdTravelStyle { get; set; }

    [Column("travel_style_name")]
    [Required]
    [MaxLength(100)]
    public string TravelStyleName { get; set; } = null!;

    public ICollection<UserTravelStyle>? UserTravelStyles { get; set; }
}