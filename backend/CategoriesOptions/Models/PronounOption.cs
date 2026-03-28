using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.CategoriesOptions.Models;

[Table("pronoun")]
public class PronounOption
{
    [Column("id_pronoun")]
    [Key]
    public int IdPronoun { get; set; }
    
    [Column("pronoun_name")]
    public string PronounName { get; set; } = null!;
}