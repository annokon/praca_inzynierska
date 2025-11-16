using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("trip_photo")]
public class TripPhoto
{
    [Column("id_trip_photo")]
    [Key]
    public int IdTripPhoto { get; set; }
    
    [Column("id_trip")]
    [Required]
    [ForeignKey(nameof(Trip))]
    public int IdTrip { get; set; }
    public Trip Trip { get; set; } = null!;

    //user that added photo
    [Column("uploaded_by")]
    [Required]
    [ForeignKey(nameof(UploadedByUser))]
    public int UploadedBy { get; set; }
    public User UploadedByUser { get; set; } = null!;

    [Column("photo_path")]
    [Required]
    [MaxLength(255)]
    public string PhotoPath { get; set; } = null!;

    [Column("description")]
    [MaxLength(500)]
    public string? Description { get; set; }

    [Column("uploaded_at")]
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}