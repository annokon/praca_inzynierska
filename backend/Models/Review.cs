using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("review")]
public class Review

{
    [Column("id_review")]
    [Key]
    public int IdReview { get; set; }

    [Column("id_user_reviewer")]
    [ForeignKey(nameof(Reviewer))]
    public int? IdUserReviewer { get; set; }
    
    
    [ForeignKey(nameof(IdUserReviewer))]
    [InverseProperty(nameof(User.ReviewsWritten))]
    public User Reviewer { get; set; } = null!;
    
    [Column("id_user_reviewed")]
    [ForeignKey(nameof(ReviewedUser))]
    public int? IdUserReviewed { get; set; }
    
    [ForeignKey(nameof(IdUserReviewed))]
    [InverseProperty(nameof(User.ReviewsReceived))]
    public User ReviewedUser { get; set; } = null!;

    [Column("rating")]
    [Range(1, 10)]
    public int Rating { get; set; }

    [Column("text")]
    [Required]
    [MaxLength(500)]
    public string? Text { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("is_reported")]
    public bool IsReported { get; set; } = false;
} 
