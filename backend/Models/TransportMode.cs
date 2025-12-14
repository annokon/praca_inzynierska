using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("transport_mode")]
public class TransportMode
{
    [Column("id_transport_mode")]
    [Key]
    public int IdTransportMode { get; set; }

    [Column("transport_mode_name")]
    [Required]
    [MaxLength(100)]
    public string TransportModeName { get; set; } = null!;

    public ICollection<UserTransportMode>? UserTransportModes { get; set; }
}