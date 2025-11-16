using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Review

{
    [Key]
    public int IdReview { get; set; }

    [ForeignKey(nameof(Reviewer))]
    public int? IdUserReviewer { get; set; }
    
    
    [ForeignKey(nameof(IdUserReviewer))]
    [InverseProperty(nameof(User.ReviewsWritten))]
    public User Reviewer { get; set; } = null!;
    
    [ForeignKey(nameof(ReviewedUser))]
    public int? IdUserReviewed { get; set; }
    
    [ForeignKey(nameof(IdUserReviewed))]
    [InverseProperty(nameof(User.ReviewsReceived))]
    public User ReviewedUser { get; set; } = null!;

    [Range(1, 10)]
    public int Rating { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Text { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsReported { get; set; } = false;
} 
