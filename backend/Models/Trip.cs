using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("trip")]
public class Trip
{
    [Column("id_trip")]
    [Key]
    public int IdTrip { get; set; }

    [Column("title")]
    [Required]
    [MaxLength(20)]  
    public string Title { get; set; } = null!;

    [Column("description")]
    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = null!;

    [Column("start_date")]
    public DateOnly? StartDate { get; set; }
    
    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("duration_days")]
    [Required]
    public int DurationDays { get; set; }

    [Column("destination")]
    [Required]
    [MaxLength(20)]
    public string? Destination { get; set; }

    [Column("budget_amount")]
    [Required]
    public decimal BudgetAmount { get; set; }

    [Column("budget_currency")]
    [Required]
    [MaxLength(3)]
    public string BudgetCurrency { get; set; }

    [Column("is_archived")]
    public bool IsArchived { get; set; } = false;

    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("is_reported")]
    public bool IsReported { get; set; } = false;

    [Column("travel_type")]
    [Required]
    [MaxLength(50)]
    public string TravelType { get; set; } = null!;

    //for TripPhoto
    public ICollection<TripPhoto>? Photos { get; set; }
}