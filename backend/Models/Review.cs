using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Review

{
    [Key]
    public int IdReview { get; set; }

    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(Reviewer))]
    public string IdUserReviewer { get; set; } = null!;
    public User Reviewer { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    [ForeignKey(nameof(ReviewedUser))]
    public string IdUserReviewed { get; set; } = null!;
    public User ReviewedUser { get; set; } = null!;

    [Range(1, 10)]
    public int Rating { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Text { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsReported { get; set; } = false;
} 
