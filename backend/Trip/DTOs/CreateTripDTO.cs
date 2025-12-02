using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.Trip.DTOs;

public class CreateTripDTO
{
    // required data
    [Required, MaxLength(20)]
    public int IdTrip { get; set; }
    
    [Required, MaxLength(20)]
    public string Title { get; set; } = null!;
    
    [Required, MaxLength(2000)]
    public string Description { get; set; } = null!;
    
    [Required, MaxLength(20)]
    public string Destination { get; set; } = null!;
    
    [Required]
    public int DurationDays { get; set; }
    
    [Required, MaxLength(50)]
    public string TravelType { get; set; } = null!;
    
    [Required, MaxLength(20)]
    public decimal BudgetAmount { get; set; }
    
    [Required]
    public string BudgetCurrency { get; set; } = null!;
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public bool IsArchived { get; set; }
    
    [Required]
    public bool IsReported { get; set; }
    
    // optional
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<TripPhoto>? Photos { get; set; }
}