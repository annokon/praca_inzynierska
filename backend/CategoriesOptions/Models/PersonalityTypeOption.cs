using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.CategoriesOptions.Models;

[Table("personality_type")]
public class PersonalityTypeOption
{
    [Column("id_personality_type")]
    [Key]
    public int IdPersonalityType { get; set; }
    
    [Column("personality_type_name")]
    public string PersonalityTypeName { get; set; } = null!;
}