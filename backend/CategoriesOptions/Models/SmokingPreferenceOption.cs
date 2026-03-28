using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.CategoriesOptions.Models;

[Table("smoking_preference")]
public class SmokingPreferenceOption
{
    [Column("id_smoking_preference")]
    [Key]
    public int IdSmokingPreference { get; set; }
    
    [Column("smoking_preference_name")]
    public string SmokingPreferenceName { get; set; } = null!;
}