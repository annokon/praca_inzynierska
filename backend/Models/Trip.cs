using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Trip
{
    [Key]
    public int IdTrip { get; set; }

    [Required]
    [MaxLength(20)]  
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(2000)]
    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public int? DurationDays { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Destination { get; set; }

    public decimal? BudgetAmount { get; set; }

    [MaxLength(3)]
    public string? BudgetCurrency { get; set; }

    public bool IsArchived { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsReported { get; set; } = false;

    [Required]
    [MaxLength(50)]
    public string? TravelType { get; set; }

    //for TripPhoto
    public ICollection<TripPhoto>? Photos { get; set; }
}