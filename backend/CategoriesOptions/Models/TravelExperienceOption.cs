using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.CategoriesOptions.Models;

[Table("travel_experience")]
public class TravelExperienceOption
{
    [Column("id_travel_experience")]
    [Key]
    public int IdTravelExperience { get; set; }
    
    [Column("travel_experience_name")]
    public string TravelExperienceName { get; set; } = null!;
}