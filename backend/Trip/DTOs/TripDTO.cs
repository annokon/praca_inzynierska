using backend.Models;

namespace backend.Trip.DTOs;

public class TripDTO
{
    // required
    public int IdTrip { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Destination { get; set; } = null!;
    public int DurationDays { get; set; }
    public string TravelType { get; set; } = null!;
    public decimal BudgetAmount { get; set; }
    public string BudgetCurrency { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsArchived { get; set; }
    public bool IsReported { get; set; }
    
    // optional
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<TripPhoto>? Photos { get; set; }
}