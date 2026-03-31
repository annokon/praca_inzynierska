using backend.Interest.DTOs;

namespace backend.Interest.Services;

public interface IInterestService
{
    Task SeedInterestsAsync();
    Task<IEnumerable<InterestDTO>> GetAllAsync();
}