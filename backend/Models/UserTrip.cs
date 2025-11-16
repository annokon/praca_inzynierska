using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("user_trip")]
public class UserTrip
{
    [Column("user_id")]
    [Required]
    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    public User User { get; set; } = null!;

    [Column("trip_id")]
    [ForeignKey(nameof(Trip))]
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    [Column("role")]
    [MaxLength(20)]
    public string? Role { get; set; }
}