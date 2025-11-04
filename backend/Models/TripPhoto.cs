using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class TripPhoto
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(Trip))]
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    //user that added photo
    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(UploadedByUser))]
    public string UploadedBy { get; set; } = null!;
    public User UploadedByUser { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string PhotoPath { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}