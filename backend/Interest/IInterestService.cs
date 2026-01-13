using backend.Interest.DTOs;

namespace backend.Interest;

public interface IInterestService
{
    Task SeedInterestsAsync();
    Task<IEnumerable<InterestDTO>> GetAllAsync();
}