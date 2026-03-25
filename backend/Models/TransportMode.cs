using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("transport_mode")]
public class TransportMode
{
    [Column("id_transport_mode")]
    [Key]
    public int IdTransportMode { get; set; }

    [Column("transport_mode_name_en")]
    [Required]
    [MaxLength(100)]
    public string TransportModeNameEn { get; set; } = null!;
    
    [Column("transport_mode_name_pl")]
    [Required]
    [MaxLength(100)]
    public string TransportModeNamePl { get; set; } = null!;

    public ICollection<UserTransportMode>? UserTransportModes { get; set; }
}