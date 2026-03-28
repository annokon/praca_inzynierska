using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.CategoriesOptions.Models;

[Table("gender")]
public class GenderOption
{
    [Column("id_gender")]
    [Key]
    public int IdGender { get; set; }
    
    [Column("gender_name")]
    public string GenderName { get; set; } = null!;
}