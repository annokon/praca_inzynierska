using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class UserTrip
{
    [MaxLength(20)]
    [ForeignKey(nameof(User))]
    public string UserUsername { get; set; } = null!;
    public User User { get; set; } = null!;

    [ForeignKey(nameof(Trip))]
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    [MaxLength(20)]
    public string? Role { get; set; }
}