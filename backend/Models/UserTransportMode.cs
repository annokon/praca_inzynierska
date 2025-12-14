using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("user_transport_mode")]
public class UserTransportMode
{
    [Column("id_transport_mode")]
    [Required]
    [ForeignKey(nameof(TransportMode))]
    public int IdTransportMode { get; set; }
    public TransportMode TransportMode { get; set; } = null!;

    [Column("id_user")]
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;
}