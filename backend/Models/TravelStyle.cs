using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("travel_style")]
public class TravelStyle
{
    [Column("id_travel_style")]
    [Key]
    public int IdTravelStyle { get; set; }

    [Column("travel_style_name_en")]
    [Required]
    [MaxLength(100)]
    public string TravelStyleNameEn { get; set; } = null!;
    
    [Column("travel_style_name_pl")]
    [Required]
    [MaxLength(100)]
    public string TravelStyleNamePl { get; set; } = null!;

    public ICollection<UserTravelStyle>? UserTravelStyles { get; set; }
}