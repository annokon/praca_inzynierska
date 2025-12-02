using System.ComponentModel.DataAnnotations;
using backend.Models;

namespace backend.Trip.DTOs;

public class UpdateTripDTO
{
    // required data
    [MaxLength(20)]
    public int? IdTrip { get; set; }
    
    [MaxLength(20)]
    public string? Title { get; set; }
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [MaxLength(20)]
    public string? Destination { get; set; }
    
    public int? DurationDays { get; set; }
    
    [MaxLength(50)]
    public string? TravelType { get; set; }
    
    [MaxLength(20)]
    public decimal? BudgetAmount { get; set; }
    
    public string? BudgetCurrency { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public bool? IsArchived { get; set; }
    
    public bool? IsReported { get; set; }
    
    // optional
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<TripPhoto>? Photos { get; set; }
}