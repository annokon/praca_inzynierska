using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.CategoriesOptions.Models;

[Table("alcohol_preference")]
public class AlcoholPreferenceOption
{
    [Column("id_alcohol_preference")]
    [Key]
    public int IdAlcoholPreference { get; set; }
    
    [Column("alcohol_preference_name")]
    public string AlcoholPreferenceName { get; set; } = null!;
}